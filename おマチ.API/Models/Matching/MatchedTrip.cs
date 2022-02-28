using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class MatchedTrip
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của chuyến đi chung
        /// <para>Cấu tạo bởi UserId + 1..n</para>
        /// </summary>
        public Guid Id { get; set; }

        public String Users { get; set; }

        public Interval StartInterval { get; set; }

        public String ArrivalTime { get; set; }

        /// <summary>
        /// Ô khởi hành
        /// </summary>
        public Cell StartCell { get; set; }

        /// <summary>
        /// Ô điểm đến
        /// </summary>
        public Cell ArrivalCell { get; set; }

        public DateTime Timestamp { get; set; }
        #endregion Attributes
    }
}
