using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Activity
{
    public class ActivityUpdateRequest
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public float Lat { get; set; }
        [Required]
        public float Lon { get; set; }
        [Required]
        public String StartTime { get; set; }
    }
}
