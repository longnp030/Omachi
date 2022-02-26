using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Helpers;
using おマチ.API.Services;

namespace おマチ.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class POIsController : ControllerBase
    {
        #region Attributes
        private readonly IPOIService _poiService;
        private readonly AppSettings _appSettings;
        #endregion Attributes

        #region Constructor
        public POIsController(IPOIService poiService, IOptions<AppSettings> appSettings)
        {
            _poiService = poiService;
            _appSettings = appSettings.Value;
        }
        #endregion Constructor

        #region CRUD Methods
        /// <summary>
        /// API lấy 1 điểm ưa thích bởi mã định danh duy nhất
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <returns>Một hoạt động</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var poi = _poiService.GetById(id);
            return Ok(poi);
        }

        /// <summary>
        /// API lấy list điểm ưa thích bởi danh mục
        /// </summary>
        /// <param name="category">Danh mục của điểm ưa thích</param>
        /// <returns>Danh sách điểm ưa thích</returns>
        [HttpGet("Filter")]
        public IActionResult GetByCategory([FromQuery] String category)
        {
            var pois = _poiService.GetByCategory(category);
            return Ok(pois);
        }

        /// <summary>
        /// API lấy list danh mục
        /// </summary>
        /// <returns>Danh sách danh mục</returns>
        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            var categories = _poiService.GetCategories();
            return Ok(categories);
        }
        #endregion CRUD Methods
    }
}
