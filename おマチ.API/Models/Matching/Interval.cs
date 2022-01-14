using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class Interval
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của khoảng thời gian
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Thời điểm bắt đầu của khoảng thời gian
        /// </summary>
        public String Start { get; set; }

        /// <summary>
        /// Thời điểm kết thúc của khoảng thời gian
        /// </summary>
        public String End { get; set; }

        #endregion Attributes
    }
}
