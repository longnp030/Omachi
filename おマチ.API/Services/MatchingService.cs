using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Authorization;
using おマチ.API.Entities;
using おマチ.API.Helpers;
using おマチ.API.Models.Matching;

namespace おマチ.API.Services
{
    public interface IMatchingService
    {
        IEnumerable<MatchedTrip> Finding(Guid userId, CarRequest carRequest);
        //IEnumerable<FoundTrip> Finding(Guid userId, CarRequest carRequest);
    }

    public class MatchingService : IMatchingService
    {
        #region Properties
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        #endregion Properties

        #region Constructor
        public MatchingService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// So khớp để tìm các chuyến đi chung khả thi
        /// </summary>
        /// <param name="userId">Mã định danh duy nhất của người dùng</param>
        /// <param name="carRequest">Thông tin yêu câu đi chung</param>
        /// <returns>Các chuyến đi chung khả thi</returns>
        public IEnumerable<MatchedTrip> Finding(Guid userId, CarRequest carRequest)
        //public IEnumerable<FoundTrip> Finding(Guid userId, CarRequest carRequest)

        {
            /*
             * Xác định
             * mappedCarRequest: Yêu cầu đi chung được ánh xạ
             * mappedActivities: Hoạt động thường ngày được ánh xạ
             * semanticGrid: Lưới ngữ nghĩa
             * timeWindow: Cửa sổ thời gian (Mặc định = 1 giờ)
             */
            var mappedCarRequest = GetMappedCarRequest(userId, carRequest);
            var mappedActivities = GetMappedActivities(userId);
            var semanticGrid = GetSemanticGrid(carRequest.Category);
            var timeWindow = TimeSpan.Parse("01:00:00");

            // Khởi tạo các chuyến đi chung có khả thi bằng list rỗng
            var foundTrips = new List<FoundTrip>();

            // Khởi tạo các điểm đón bằng list rỗng
            var pickups = new List<Cell>();

            // Lặp từng hoạt động trong các hoạt động thường ngày được ánh xạ
            foreach (MappedActivity mappedActivity in mappedActivities)
            {
                /*
                 * Nếu hoạt động này nằm trong cùng ô đón của yêu cầu đi chung và
                 * thời điểm bắt đầu hoạt động nằm trong khoảng thời gian xuất phát và kết thúc của yêu cầu đi chung +- cửa sổ thời gian
                 * thì chọn nó làm ô điểm đón
                 */
                /* Bản gốc
                if (mappedActivity.CellId == mappedCarRequest.CellStartId 
                    && _context.Intervals.Find(mappedCarRequest.IntvStartId).Start - timeWindow <= _context.Intervals.Find(mappedActivity.IntvId).Start
                    && _context.Intervals.Find(mappedActivity.IntvId).Start <= _context.Intervals.Find(mappedCarRequest.IntvEndId).Start + timeWindow)
                 */
                /* Sửa MappedActivity
                if (mappedActivity.Cell.Id == mappedCarRequest.CellStartId
                    && _context.Intervals.Find(mappedCarRequest.IntvStartId).Start - timeWindow <= mappedActivity.Intv.Start
                    && mappedActivity.Intv.Start <= _context.Intervals.Find(mappedCarRequest.IntvEndId).Start + timeWindow)
                 */
                /*
                 * 13/01/2022
                 * Sửa MappedActivity -> Sửa MappedCarRequest
                 */
                if (mappedActivity.Cell.Id == mappedCarRequest.CellStart.Id
                    && ToTime(mappedCarRequest.IntvStart.Start) - timeWindow <= ToTime(mappedActivity.Intv.Start)
                    && ToTime(mappedActivity.Intv.Start) <= ToTime(mappedCarRequest.IntvArrival.Start) + timeWindow)
                {
                    // pickups.Add(_context.Cells.Find(mappedActivity.CellId)); // Bản gốc
                    pickups.Add(mappedActivity.Cell); // Sửa MappedActivity
                }
            }

            // Lặp từng ô đón trong list các ô điểm đón
            foreach (Cell p in pickups)
            {
                // Khởi tạo điểm đến bằng một list rỗng
                var destinations = new List<Cell>();

                // Lặp từng hoạt động trong các hoạt động thường ngày được ánh xạ
                foreach (MappedActivity mappedActivity in mappedActivities)
                {
                    /*
                     * Khởi tạo một biến để kiểm tra có ô không gian ngữ nghĩa nào 
                     * chứa hoạt động thường ngày được ánh xạ đang xét hay không
                     * 
                     * Mã định danh của ô của hoạt động = Mã định danh ô ngữ nghĩa
                     * Thời gian bắt đầu hoạt động < Thời gian kết thúc của ô ngữ nghĩa
                     * Nếu tồn tại thì mới có điểm đến
                     */
                    var anyActExistsInSemanticGrid = false;
                    foreach (SemanticCell sc in semanticGrid)
                    {
                        /* Bản gốc
                        if (sc.StartTime <= _context.Intervals.Find(mappedActivity.IntvId).Start
                            && _context.Intervals.Find(mappedActivity.IntvId).End <= sc.EndTime)
                         */
                        /*
                         * Sửa MappedActivity
                         */
                        if (mappedActivity.Cell.Id == sc.CellId
                            && sc.StartTime <= ToTime(mappedActivity.Intv.Start)
                            && ToTime(mappedActivity.Intv.End) <= sc.EndTime)
                        {
                            anyActExistsInSemanticGrid = true;
                            break;
                        }
                    }

                    /*
                     * Nếu hoạt động có tồn tại trong lưới ngữ nghĩa và
                     * thời điểm bắt đầu hoạt động < thời gian đến của yêu cầu đi chung
                     */
                    /* Bản gốc
                    if (_context.Intervals.Find(mappedCarRequest.IntvStartId).Start - timeWindow <= _context.Intervals.Find(mappedActivity.IntvId).Start
                        && _context.Intervals.Find(mappedActivity.IntvId).Start <= _context.Intervals.Find(mappedCarRequest.IntvEndId).Start + timeWindow
                        && anyActExistsInSemanticGrid)
                     */
                    /* Sửa MappedActivity
                    if (_context.Intervals.Find(mappedCarRequest.IntvStartId).Start - timeWindow <= mappedActivity.Intv.Start
                        && mappedActivity.Intv.Start <= _context.Intervals.Find(mappedCarRequest.IntvEndId).Start + timeWindow
                        && anyActExistsInSemanticGrid)
                     */
                    /*
                     * Sửa MappedActivity -> Sửa MappedCarRequest
                     */
                    if (anyActExistsInSemanticGrid
                        && ToTime(mappedCarRequest.IntvStart.Start) - timeWindow <= ToTime(mappedActivity.Intv.Start)
                        && ToTime(mappedActivity.Intv.Start) <= ToTime(mappedCarRequest.IntvArrival.Start) + timeWindow
                        // Lúc đến POI thì hoạt động ở ô đó chưa kết thúc
                        //&& ToTime(mappedCarRequest.IntvArrival.Start) < ToTime(mappedActivity.Intv.End)
                        )
                    {
                        // destinations.Add(_context.Cells.Find(mappedActivity.CellId)); // Bản gốc
                        destinations.Add(mappedActivity.Cell); // Sửa MappedActivity
                    }
                }

                /*
                 * Số yêu cầu đi chung khả thi = Số điểm đón * Số điểm đến 
                 * |lstCarpooling| = |p|x|d|
                 * 
                 * Chưa thêm code add vào CSDL
                 * Modified: 27/02/2022: Added code to insert into DB
                 */
                foreach (Cell d in destinations)
                {
                    var foundTrip = new FoundTrip
                    {
                        UserId = userId,
                        StartIntervalId = mappedCarRequest.IntvStart.Id,
                        ArrivalTime = carRequest.ArrivalTime,
                        StartCellId = p.Id,
                        EndCellId = d.Id,
                        Timestamp = DateTime.Now
                    };
                    foundTrips.Add(foundTrip);
                    _context.FoundTrip.Add(foundTrip);
                    _context.SaveChanges();
                }
            }

            //return foundTrips;
            return Matching(foundTrips);
        }

        private IEnumerable<MatchedTrip> Matching(IEnumerable<FoundTrip> foundTrips)
        {
            // foundTrips: Current user's found trips
            /* GOAL: Query other users' found trips 
             *       that have same Start Interval, Start Cell, End Cell
             *       [and found time less than or greater than foundTrip's Timestamp one minute.]
             *       Combine those found trip into one MatchedTrip
             *       -> Return that one MatchedTrip
             */
            
            var matchedTrips = new List<MatchedTrip>();

            foreach (FoundTrip foundTrip in foundTrips)
            {
                var similarTrips = _context.FoundTrip.Where(t => t.UserId != foundTrip.UserId)
                                  .Where(t => t.StartCellId == foundTrip.StartCellId)
                                  .Where(t => t.EndCellId == foundTrip.EndCellId)
                                  .Where(t => t.StartIntervalId == foundTrip.StartIntervalId)
                                  //.Where(t => ToTime(t.ArrivalTime) <= ToTime(foundTrip.ArrivalTime) - ToTime("00:15:00")
                                  //            || ToTime(t.ArrivalTime) >= ToTime(foundTrip.ArrivalTime) + ToTime("00:15:00"))
                                  //.Where(t => t.Timestamp >= foundTrip.Timestamp.AddMinutes(-1))
                                  .ToList();

                var users = foundTrip.UserId.ToString();
                var minArrivalTime = ToTime(foundTrip.ArrivalTime);
                var maxArrivalTime = ToTime(foundTrip.ArrivalTime);
                foreach (FoundTrip similarTrip in similarTrips)
                {
                    //if (ToTime(foundTrip.ArrivalTime) - ToTime("00:15:00") <= ToTime(similarTrip.ArrivalTime)
                    //    && ToTime(similarTrip.ArrivalTime) <= ToTime(foundTrip.ArrivalTime) + ToTime("00:15:00"))
                    //{
                    //    users = users + ", " + similarTrip.UserId.ToString();
                    //    if (ToTime(similarTrip.ArrivalTime) < minArrivalTime)
                    //    {
                    //        minArrivalTime = ToTime(similarTrip.ArrivalTime);
                    //    }
                    //    if (ToTime(similarTrip.ArrivalTime) > maxArrivalTime)
                    //    {
                    //        maxArrivalTime = ToTime(similarTrip.ArrivalTime);
                    //    }
                    //}
                    users = users + ", " + similarTrip.UserId.ToString();
                    if (ToTime(similarTrip.ArrivalTime) < minArrivalTime)
                    {
                        minArrivalTime = ToTime(similarTrip.ArrivalTime);
                    }
                    if (ToTime(similarTrip.ArrivalTime) > maxArrivalTime)
                    {
                        maxArrivalTime = ToTime(similarTrip.ArrivalTime);
                    }
                }

                var matchedTrip = new MatchedTrip
                {
                    Users = users,
                    StartInterval = _context.Interval.Find(foundTrip.StartIntervalId),
                    ArrivalTime = minArrivalTime.ToString() + " - " + maxArrivalTime.ToString(), 
                    StartCell = _context.Cell.Find(foundTrip.StartCellId),
                    ArrivalCell = _context.Cell.Find(foundTrip.EndCellId),
                    Timestamp = foundTrip.Timestamp
                };

                matchedTrips.Add(matchedTrip);
            }

            return matchedTrips;
        }

        #endregion Methods

        #region Helper Methods
        /// <summary>
        /// Ánh xạ các hoạt động thường ngày
        /// </summary>
        /// <param name="userId">Mã định danh duy nhất của người dùng</param>
        /// <returns>Chuỗi hoạt động thường ngày được ánh xạ</returns>
        private IEnumerable<MappedActivity> GetMappedActivities(Guid userId)
        {
            var activities = _context.Activity.Where(act => act.UserId == userId).OrderBy(act => act.StartTime).ToList();
            var setOfIntvals = _context.Interval.ToList();
            var grid = _context.Cell.ToList();

            var mappedActivities = new List<MappedActivity>();

            var i = 1;
            foreach (Activity activity in activities)
            {
                var mappedActivity = new MappedActivity
                {
                    UserId = userId,
                    Order = i
                };
                i++;

                foreach (Cell cell in grid)
                {
                    if (activity.Lat <= cell.TopLeftLat && activity.Lat > cell.BotRightLat
                        && activity.Lon >= cell.TopLeftLon && activity.Lon < cell.BotRightLon)
                    {
                        // mappedActivity.CellId = cell.Id; // Bản gốc
                        mappedActivity.Cell = cell; // Sửa MappedActivity
                        break;
                    }
                }

                foreach (Interval interval in setOfIntvals)
                {
                    if (ToTime(activity.StartTime) >= ToTime(interval.Start) && ToTime(activity.StartTime) < ToTime(interval.End))
                    {
                        // mappedActivity.IntvId = interval.Id; // Bản gốc
                        mappedActivity.Intv = interval; // Sửa MappedActivity
                        break;
                    }
                }

                mappedActivities.Add(mappedActivity);
            }

            return mappedActivities;
        }

        /// <summary>
        /// Ánh xạ yêu cầu đi chung xe
        /// </summary>
        /// <param name="userId">Mã định danh duy nhất của người dùng</param>
        /// <param name="carRequest">Yêu cầu đi chung xe</param>
        /// <returns>Yêu cầu đi chung xe được ánh xạ</returns>
        private MappedCarRequest GetMappedCarRequest(Guid userId, CarRequest carRequest)
        {
            var activities = _context.Activity.Where(act => act.UserId == userId).OrderBy(act => act.StartTime).ToList();
            var setOfIntvals = _context.Interval.ToList();
            var grid = _context.Cell.ToList();

            var mappedCarRequest = new MappedCarRequest
            {
                UserId = userId,
                // Category = carRequest.PointOfInterest.Category // Trước 27/02/2022
                Category = carRequest.Category // Sửa CarRequest
            };

            foreach (Cell cell in grid)
            {
                if (carRequest.StartLat <= cell.TopLeftLat && carRequest.StartLat > cell.BotRightLat
                    && carRequest.StartLon >= cell.TopLeftLon && carRequest.StartLon < cell.BotRightLon)
                {
                    // mappedCarRequest.CellStartId = cell.Id; // Bản gốc
                    mappedCarRequest.CellStart = cell; // Sửa MappedCarRequest
                    break;
                }
            }

            /*
             * Commented on 27/02/2022
             * Changed CarRequest
            foreach (Cell cell in grid)
            {
                if (carRequest.PointOfInterest.Lat <= cell.TopLeftLat && carRequest.PointOfInterest.Lat > cell.BotRightLat
                    && carRequest.PointOfInterest.Lon >= cell.TopLeftLon && carRequest.PointOfInterest.Lon < cell.BotRightLon)
                {
                    // mappedCarRequest.CellEndId = cell.Id; // Bản gốc
                    mappedCarRequest.CellEnd = cell; // Sửa MappedCarRequest
                    break;
                }
            }
            */

            foreach (Interval interval in setOfIntvals)
            {
                if (ToTime(carRequest.StartTime) >= ToTime(interval.Start) && ToTime(carRequest.StartTime) < ToTime(interval.End))
                {
                    // mappedCarRequest.IntvStartId = interval.Id; // Bản gốc
                    mappedCarRequest.IntvStart = interval; // Sửa MappedCarRequest
                    break;
                }
            }

            /*
             * Commented on 27/02/2022
             * Changed CarRequest
            foreach (Interval interval in setOfIntvals)
            {
                if (ToTime(carRequest.PointOfInterest.CloseTime) >= ToTime(interval.Start) && ToTime(carRequest.PointOfInterest.CloseTime) < ToTime(interval.End))
                {
                    // mappedCarRequest.IntvEndId = interval.Id; // Bản gốc
                    mappedCarRequest.IntvEnd = interval; // Sửa MappedCarRequest
                    break;
                }
            }
            */
            /* Changed CarRequest
             * Created: 27/02/2022
             * */
            foreach (Interval interval in setOfIntvals)
            {
                if (ToTime(carRequest.ArrivalTime) >= ToTime(interval.Start) && ToTime(carRequest.ArrivalTime) < ToTime(interval.End))
                {
                    mappedCarRequest.IntvArrival = interval;
                    break;
                }
            }

            return mappedCarRequest;
        }

        /// <summary>
        /// Ánh xạ điểm ưa thích có loại category và tập hợp khoảng thời gian vào lưới không gian
        /// </summary>
        /// <param name="category">Loại điểm ưa thích</param>
        /// <returns>Lưới ngữ nghĩa</returns>
        private IEnumerable<SemanticCell> GetSemanticGrid(String category)
        {
            var POIs = _context.PointOfInterest.Where(poi => poi.Category == category).ToList();
            var setOfIntvals = _context.Interval.ToList();
            var grid = _context.Cell.ToList();

            var semanticGrid = new List<SemanticCell>();

            foreach (Cell cell in grid)
            {
                var semanticCell = new SemanticCell
                {
                    StartTime = TimeSpan.Parse("00:00:01"),
                    EndTime = TimeSpan.Parse("23:59:59")
                };

                foreach (PointOfInterest poi in POIs)
                {
                    if (poi.Lat <= cell.TopLeftLat && poi.Lat > cell.BotRightLat
                        && poi.Lon >= cell.TopLeftLon && poi.Lon < cell.BotRightLon)
                    {
                        semanticCell.CellId = cell.Id;

                        if (ToTime(poi.OpenTime) > semanticCell.StartTime)
                        {
                            semanticCell.StartTime = ToTime(poi.OpenTime);
                        }
                        if (ToTime(poi.CloseTime) < semanticCell.EndTime)
                        {
                            semanticCell.EndTime = ToTime(poi.CloseTime);
                        }
                    }                 
                }
                if (semanticCell.CellId.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    semanticGrid.Add(semanticCell);
                }
            }

            
            foreach (var item in semanticGrid)
            {
                Console.WriteLine(string.Format("Item: {0} - Cost: {1} - {2}", item.StartTime, item.EndTime, item.CellId.ToString()));
            }

            return semanticGrid;
        }

        private static TimeSpan ToTime(String time)
        {
            var timeSpan = TimeSpan.Parse(time);
            return timeSpan;
        }

        #endregion Helper Methods
    }
}
