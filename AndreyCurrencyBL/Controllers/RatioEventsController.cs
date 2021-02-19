using AndreyCurrenclyShared.Models;
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
    [ApiController]
    // [Route("signalr/[controller]")]
    [Route("api/chart")]
    public class RatioEventsController : ControllerBase
    {
        private IHubContext<ChartHub> Hub;
        public RatioEventsController(IHubContext<ChartHub>  _hub)
        {
           Hub = _hub;
        }

        [HttpGet]
        [Route("simulate")]
        public async Task<ActionResult<List<CurrencyRatioChange>>>  SimulateEventent()
        {

            List<CurrencyRatioChange> data = ChangeRatioSimulatorMasnager.GetChanges();

            await Task.Delay(1);
           // await HubFacade.changeRatios(data);

          
           return Ok(data);
        }
    }
}
