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
        public async Task JoinChatGroup(Guid matchedTripId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, matchedTripId.ToString());
        }
        public Task SendMessage(Guid userId, String message)
        {
            return Clients.All.SendMessage(userId, message);
        }
    }
    #endregion Main Hub Class
}
