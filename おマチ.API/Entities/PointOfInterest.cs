using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Entities
{
    public class PointOfInterest
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của điểm ưa thích
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Tên điểm ưa thích
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Vĩ độ điểm ưa thích
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        /// Kinh độ điểm ưa thích
        /// </summary>
        public float Lon { get; set; }

        /// <summary>
        /// Loại điểm ưa thích
        /// </summary>
        public String Category { get; set; }

        /// <summary>
        /// Giờ mở cửa của điểm ưa thích
        /// </summary>
        public String OpenTime { get; set; }

        /// <summary>
        /// Giờ đóng cửa của điểm ưa thích
        /// </summary>
        public String CloseTime { get; set; }

        #endregion Attributes
    }
}
