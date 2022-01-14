using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Authorization;
using おマチ.API.Entities;
using おマチ.API.Helpers;
using おマチ.API.Models.Activity;

namespace おマチ.API.Services
{
    #region Interface
    public interface IActivityService
    {
        IEnumerable<Activity> GetAll();
        void Add(Guid userId, ActivityAddRequest model);
        void Update(Guid id, ActivityUpdateRequest model);
        Activity GetById(Guid id);
        IEnumerable<Activity> GetUserActivities(Guid userId);
        void Delete(Guid id);
    }

    #endregion Interface

    #region Main Class
    public class ActivityService : IActivityService
    {
        #region Properties
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        #endregion Properties

        #region Constructor
        public ActivityService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// Lấy thông tin toàn bộ hoạt động
        /// </summary>
        /// <returns>Toàn bộ hoạt động</returns>
        public IEnumerable<Activity> GetAll()
        {
            return _context.Activity;
        }

        /// <summary>
        /// Thêm mới một hoạt động
        /// </summary>
        /// <param name="model">Thông tin hoạt động</param>
        public void Add(Guid userId, ActivityAddRequest model)
        {
            // map model to new activity object
            var activity = _mapper.Map<Activity>(model);
            activity.UserId = userId;

            // save user
            _context.Activity.Add(activity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Chỉnh sửa một hoạt đông
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <param name="model">Thông tin mới của hoạt động</param>
        public void Update(Guid id, ActivityUpdateRequest model)
        {
            var activity = GetActivity(id);

            // copy model to user and save
            _mapper.Map(model, activity);
            _context.Activity.Update(activity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Lấy một hoạt động theo mã định danh duy nhất
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <returns>Một hoạt động</returns>
        public Activity GetById(Guid id)
        {
            return GetActivity(id);
        }

        /// <summary>
        /// Lấy toàn bộ hoạt động của người dùng có mã định danh duy nhất userId
        /// </summary>
        /// <param name="userId">Mã định danh duy nhất của người dùng</param>
        /// <returns>Các hoạt động của người dùng</returns>
        public IEnumerable<Activity> GetUserActivities(Guid userId)
        {
            var activities = _context.Activity.Where(a => a.UserId == userId).ToList();
            if (activities.Count == 0)
            {
                throw new KeyNotFoundException("No activity found");
            }

            return activities;
        }

        /// <summary>
        /// Xóa một hoạt động
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        public void Delete(Guid id)
        {
            var activity = GetActivity(id);
            _context.Activity.Remove(activity);
            _context.SaveChanges();
        }
        #endregion Methods

        #region Helper Methods
        /// <summary>
        /// Lấy một họat động
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của hoạt động</param>
        /// <returns>Một hoạt động</returns>
        private Activity GetActivity(Guid id)
        {
            var activity = _context.Activity.Find(id);
            if (activity == null)
            {
                throw new KeyNotFoundException("Activity not found");
            }

            return activity;
        }
        #endregion Helper Methods
    }
    #endregion Main Class
}
