using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Entities
{
    public class Activity
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của Hoạt động trong một Chuỗi hoạt động
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã định danh duy nhất của Người dùng
        /// Khóa ngoại nối tới khóa chính bảng "User"
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tên hoạt động
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Vĩ độ họat động
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        /// Kinh độ hoạt động
        /// </summary>
        public float Lon { get; set; }

        /// <summary>
        /// Thời gian bắt đầu hoạt động
        /// </summary>
        public String StartTime { get; set; }

        #endregion Attributes
    }
}
