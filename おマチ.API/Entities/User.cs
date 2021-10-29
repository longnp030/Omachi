using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace おマチ.API.Entities
{
    public class User
    {
        #region Properties

        #region Identity

        public Guid Id { get; set; }
        public String IdentityCode { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }

        [JsonIgnore]
        public String PasswordHash { get; set; }

        #endregion Identity

        #region BasicInformation
        public DateTime DateOfBirth { get; set; }
        public String Gender { get; set; }
        public String Nation { get; set; }
        public String IpAddress { get; set; }
        public String AboutMe { get; set; }
        public String Interests { get; set; }
        public String Languages { get; set; }
        public DateTime JoinDate { get; set; }

        #endregion BasicInformation

        #endregion Properties
    }
}
