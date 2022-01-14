using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Authorization;
using おマチ.API.Helpers;
using おマチ.API.Models.Activity;
using おマチ.API.Services;

namespace おマチ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController : ControllerBase
    {
        #region Attributes
        private readonly IActivityService _activityService;
        private readonly AppSettings _appSettings;
        #endregion Attributes

        #region Constructor
        public ActivitiesController(IActivityService activityService, IOptions<AppSettings> appSettings)
        {
            _activityService = activityService;
            _appSettings = appSettings.Value;
        }
        #endregion Constructor

        #region CRUD Methods
        /// <summary>
        /// API lấy 1 hoạt động bởi mã định danh duy nhất
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <returns>Một hoạt động</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var activity = _activityService.GetById(id);
            return Ok(activity);
        }

        /// <summary>
        /// Chỉnh sửa một hoạt động có mã định danh duy nhất id
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <param name="model">Thông tin mới của hoạt động</param>
        /// <returns>HTTP Response</returns>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ActivityUpdateRequest model)
        {
            _activityService.Update(id, model);
            return Ok(new { message = "Activity updated successfully" });
        }

        /// <summary>
        /// Xóa một hoạt động có mã định danh duy nhất id
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <returns>HTTP Response</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _activityService.Delete(id);
            return Ok(new { message = "Activity deleted successfully" });
        }
        #endregion CRUD Methods
    }
}
