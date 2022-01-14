using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class MappedActivity
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Số thứ tự của hoạt động trong chuỗi
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Ô không gian mà hoạt động nằm trong
        /// </summary>
        // public Guid CellId { get; set; }
        public Cell Cell { get; set; }

        /// <summary>
        /// Khoảng thời gian mà hoạt động nằm trong
        /// </summary>
        // public Guid IntvId { get; set; }
        public Interval Intv { get; set; }
        #endregion Attributes
    }
}
