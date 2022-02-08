using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Entities;

namespace おマチ.API.Models.Matching
{
    public class CarRequest
    {
        #region Attributes

        /// <summary>
        /// Mã định danh duy nhất của yêu cầu đi chung xe
        /// <para>CaRequest.Id -> Booking.Id</para>
        /// <para>Modified: 15/01/2022: Thêm mới</para>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã định danh duy nhất của người dùng tạo yêu cầu đi chung
        /// <para>CarRequest.UserId -> Booking.UserId</para>
        /// <para>Modified: 15/01/2022: Thêm mới</para>
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Vĩ độ điểm xuất phát
        /// </summary>
        public float StartLat { get; set; }

        /// <summary>
        /// Kinh độ điểm xuất phát
        /// </summary>
        public float StartLon { get; set; }

        /// <summary>
        /// Thời điểm xuất phát
        /// </summary>
        public String StartTime { get; set; }

        /// <summary>
        /// Điểm ưa thích làm đích đến
        /// <para>Shadow property = PointOfInterest.Id trong CSDL</para>
        /// <para>CarRequest.PointOfInterest.Name -> Booking.Name</para>
        /// <para>Modified: 15/01/2022: Sửa từ "EndPOI" sang "PointOfInterest"</para>
        /// </summary>
        public PointOfInterest PointOfInterest { get; set; }

        /// <summary>
        /// Thời điểm tạo yêu cầu đi chung
        /// <para>CarRequest.Timestamp -> Booking.Timestamp</para>
        /// <para>Modified: 15/01/2022: Thêm mới</para>
        /// </summary>
        public String Timestamp { get; set; }

        #endregion Attributes
    }
}
