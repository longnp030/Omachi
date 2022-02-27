using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class FoundTrip
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của chuyến đi chung
        /// <para>Cấu tạo bởi UserId + 1..n</para>
        /// </summary>
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid StartIntervalId { get; set; }

        /// <summary>
        /// Ô khởi hành
        /// </summary>
        public Guid StartCellId { get; set; }

        /// <summary>
        /// Ô điểm đến
        /// </summary>
        public Guid EndCellId { get; set; }

        public DateTime Timestamp { get; set; }
        #endregion Attributes
    }
}
