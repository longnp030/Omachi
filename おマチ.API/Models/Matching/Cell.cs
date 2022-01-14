using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Matching
{
    public class Cell
    {
        #region Attributes
        /// <summary>
        /// Mã định danh duy nhất của ô không gian
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Vĩ độ góc trái trên của ô
        /// </summary>
        public float TopLeftLat { get; set; }

        /// <summary>
        /// Kinh độ góc trái trên của ô
        /// </summary>
        public float TopLeftLon { get; set; }

        /// <summary>
        /// Vĩ độ góc phải dưới của ô
        /// </summary>
        public float BotRightLat { get; set; }

        /// <summary>
        /// Kinh độ góc phải dưới của ô
        /// </summary>
        public float BotRightLon { get; set; }

        #endregion Attributes
    }
}
