using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Entities;

namespace おマチ.API.Models.Matching
{
    public class CarRequest
    {
        #region Attributes
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
        /// </summary>
        public PointOfInterest EndPOI { get; set; }

        #endregion Attributes
    }
}
