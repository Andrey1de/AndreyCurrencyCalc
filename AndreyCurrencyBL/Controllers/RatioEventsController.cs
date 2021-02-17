using AndreyCurrencyBL.HubConfig;
using AndreyCurrencyBL.TimerFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreyCurrencyBL.Controllers
{
    [Route("api/sigalr/[controller]")]
    [ApiController]
    public class RatioEventsController : ControllerBase
    {
        private IHubContext<RatioEnentsHub> _hub;
        public RatioEventsController(IHubContext<RatioEnentsHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        [Route("getevent")]
        public IActionResult GetEvent()
        {

            List<AndreyCurrenclyShared.Models.CurrencyRatioCallback> data = ChangeRatioSimulatorMasnager.GetData();
            var timerManager = new TimerManager(() => 
                _hub.Clients.All.SendAsync("changeratios", data));
            return Ok(new { Message = "Request Completed" });
        }
    }
}
