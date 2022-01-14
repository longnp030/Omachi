using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class SemanticCell
    {
        #region Attributes

        /// <summary>
        /// Mã định danh duy nhất của ô
        /// </summary>
        public Guid CellId { get; set; }

        /// <summary>
        /// Thời gian bắt đầu (thời gian mở cửa sớm nhất của một điểm ưa thích)
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc (thời gian đóng cửa muộn nhất của một điểm ưa thích)
        /// </summary>
        public TimeSpan EndTime { get; set; }

        #endregion Attributes
    }
}
