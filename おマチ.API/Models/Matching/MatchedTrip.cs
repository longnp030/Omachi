using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class MatchedTrip
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của chuyến đi chung
        /// <para>Cấu tạo bởi UserId + 1..n</para>
        /// </summary>
        public Guid Id { get; set; }

        public String Users { get; set; }

        public Guid StartIntervalId { get; set; }

        public String StartTime { get; set; }

        public String ArrivalTime { get; set; }

        ///// <summary>
        ///// Ô khởi hành
        ///// </summary>
        public Guid StartCellId { get; set; }

        ///// <summary>
        ///// Ô điểm đến
        ///// </summary>
        public Guid ArrivalCellId { get; set; }

        public String POI { get; set; }

        public DateTime Timestamp { get; set; }

        public Boolean Status { get; set; }
        #endregion Attributes
    }
}
