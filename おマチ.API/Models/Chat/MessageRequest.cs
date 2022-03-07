using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Models.Chat
{
    public class MessageRequest
    {
        public Guid UserId { get; set; }

        public String Message { get; set; }
    }
}
