using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Authorization;
using おマチ.API.Helpers;
using おマチ.API.Models.Activity;
using おマチ.API.Models.Matching;
using おマチ.API.Models.User;
using おマチ.API.Services;

namespace おマチ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IActivityService _activityService;
        private IMatchingService _matchingService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IActivityService activityService, IMatchingService matchingService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _activityService = activityService;
            _matchingService = matchingService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registered successfully" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpGet("{id}/car/own")]
        public IActionResult CheckCar(Guid id)
        {
            var car = _userService.CheckCar(id);
            return Ok(car);
        }

        [HttpPatch("{id}/car")]
        public IActionResult AddAndOrUpdateCar(Guid id, CarModel model)
        {
            _userService.AddAndOrUpdateCar(id, model);
            return Ok(new { message = "Car updated successfully" });
        }

        [HttpPatch("{id}")]
        public IActionResult Update(Guid id, UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Ok(new { message = "User deleted successfully" });
        }

        /// <summary>
        /// API lấy toàn bộ Hoạt động của người dùng có mã định danh duy nhất id
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của người dùng</param>
        /// <returns>Toàn bộ Hoạt động của người dùng</returns>
        [HttpGet("{id}/activities")]
        public IActionResult GetUserActivities(Guid id)
        {
            var activities = _activityService.GetUserActivities(id);
            if (activities.Count() > 0)
            {
                return Ok(activities);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Thêm mới một hoạt động
        /// </summary>
        /// <param name="model">Thông tin của hoạt động</param>
        /// <returns>HTTP Response</returns>
        [HttpPost("{id}/activities")]
        public IActionResult AddActivity(Guid id, ActivityAddRequest model)
        {
            _activityService.Add(id, model);
            return Ok(new { message = "Activity added successfully" });
        }

        /// <summary>
        /// API so khớp chuyến đi chung cho người dùng có mã định danh duy nhất id
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của người dùng</param>
        /// <param name="carRequest">Thông tin chuyến đi chung xe</param>
        /// <returns>Các chuyến đi chung xe có khả năng</returns>
        [HttpPost("{id}/matching")]
        public IActionResult Matching(Guid id, [FromBody] CarRequest carRequest)
        {
            var lstCarpooling = _matchingService.Finding(id, carRequest);
            return Ok(lstCarpooling);
        }
    }
}
