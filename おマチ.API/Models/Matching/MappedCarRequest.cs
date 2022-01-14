using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class MappedCarRequest
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Ô khởi hành
        /// <para>Modified: 13/01/2022: Thay đổi từ Guid sang object Cell</para>
        /// </summary>
        // public Guid CellStartId { get; set; }
        public Cell CellStart { get; set; }

        /// <summary>
        /// Khoảng thời gian khởi hành
        /// <para>Modified: 13/01/2022: Thay đổi từ Guid sang object Interval</para>
        /// </summary>
        // public Guid IntvStartId { get; set; }
        public Interval IntvStart { get; set; }

        /// <summary>
        /// Ô điểm đến
        /// <para>Modified: 13/01/2022: Thay đổi từ Guid sang object Cell</para>
        /// </summary>
        // public Guid CellEndId { get; set; }
        public Cell CellEnd { get; set; }

        /// <summary>
        /// Khoảng thời gian đến
        /// <para>Modified: 13/01/2022: Thay đổi từ Guid sang object Interval</para>
        /// </summary>
        // public Guid IntvEndId { get; set; }
        public Interval IntvEnd { get; set; }

        /// <summary>
        /// Loại điểm ưa thích
        /// </summary>
        public String Category { get; set; }
        #endregion Attributes
    }
}
