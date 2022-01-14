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
        public String Id { get; set; }

        /// <summary>
        /// Ô khởi hành
        /// </summary>
        public Cell Start { get; set; }

        /// <summary>
        /// Ô điểm đến
        /// </summary>
        public Cell End { get; set; }
        #endregion Attributes
    }
}
