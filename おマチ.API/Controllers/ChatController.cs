using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Authorization;
using おマチ.API.Helpers;
using おマチ.API.Hubs;
using おマチ.API.Models.Chat;

namespace おマチ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ChatController : ControllerBase
    {
        #region Attributes
        private readonly AppSettings _appSettings;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        #endregion Attributes

        #region Constructor
        public ChatController(IOptions<AppSettings> appSettings, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _appSettings = appSettings.Value;
            _hubContext = hubContext;
        }
        #endregion

        #region Methods
        [HttpPost("chat")]
        public IActionResult SendMessage([FromBody] MessageRequest messageRequest)
        {
            _hubContext.Clients.All.SendMessage(messageRequest.UserId, messageRequest.Message);
            return Ok();
        }
        #endregion Methods
    }
}
