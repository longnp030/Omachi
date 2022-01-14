using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace おマチ.API.Entities
{
    public class User
    {
        #region Attributes

        #region Identity

        /// <summary>
        /// Mã định danh duy nhất của người dùng
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Địa chỉ email của người dùng
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// Mật khẩu đã hashed
        /// </summary>
        [JsonIgnore]
        public String PasswordHash { get; set; }

        #endregion Identity

        #region BasicInformation

        /// <summary>
        /// Đường dẫn đến hình ảnh đại diện của người dùng
        /// </summary>
        public String Avatar { get; set; }

        /// <summary>
        /// Vai trò của người dùng
        /// 0 - Hành khách; 1 - Tài xế
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// Ngày giờ tạo bản ghi trong CSDL
        /// Ngày giờ tạo tài khoản trên hệ thống
        /// </summary>
        public DateTime Timestamp { get; set; }

        #endregion BasicInformation

        #endregion Attributes
    }
}
