using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Models.Chat;

namespace おマチ.API.Hubs
{
    #region Interface
    public interface IChatHub
    {
        Task SendMessage(Guid userId, String message);
    }
    #endregion Interface

    #region Main Hub Class
    public class ChatHub : Hub<IChatHub>
    {
        
    }
    #endregion Main Hub Class
}
