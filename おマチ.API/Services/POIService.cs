using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Authorization;
using おマチ.API.Entities;
using おマチ.API.Helpers;

namespace おマチ.API.Services
{
    #region Interface
    public interface IPOIService
    { 
        PointOfInterest GetById(Guid id);
        IEnumerable<PointOfInterest> GetByCategory(String category);
        public IEnumerable<String> GetCategories();
    }
    #endregion Interface

    #region Main Class
    public class POIService : IPOIService
    {
        #region Properties
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        #endregion Properties

        #region Constructor
        public POIService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// Lấy một hoạt động theo mã định danh duy nhất
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <returns>Một hoạt động</returns>
        public PointOfInterest GetById(Guid id)
        {
            var poi = _context.PointOfInterest.Find(id);
            if (poi == null)
            {
                throw new KeyNotFoundException("Activity not found");
            }

            return poi;
        }

        /// <summary>
        /// Lấy điểm ưa thích theo danh mục
        /// </summary>
        /// <param name="category">danh mục của điểm ưa thích</param>
        /// <returns>điểm ưa thích</returns>
        public IEnumerable<PointOfInterest> GetByCategory(String category)
        {
            var pois = _context.PointOfInterest.Where(a => a.Category == category).ToList();
            if (pois.Count == 0)
            {
                throw new KeyNotFoundException("No poi found");
            }

            return pois;
        }

        /// <summary>
        /// Lấy list danh mục
        /// </summary>
        /// <returns>Danh sách danh mục</returns>
        public IEnumerable<String> GetCategories()
        {
            var categories = _context.PointOfInterest.Select(a => a.Category).Distinct().ToList();
            if (categories.Count == 0)
            {
                throw new KeyNotFoundException("No category found");
            }

            return categories;

        }
        #endregion Methods
    }
    #endregion Main Class
}
