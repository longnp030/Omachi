using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.User
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public String Email { get; set; }
        public String JwtToken { get; set; }
    }
}
