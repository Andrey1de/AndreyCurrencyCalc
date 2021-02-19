using AndreyCurrenclyShared.Models;
using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndreyCurrencyBL.HubConfig
{

     public interface IRatioCallback
    {
        [HubMethodName("SendMessageToUser")]
        Task changeRatios(List<CurrencyRatioChange> listCallb);

    }

    public class RatioEnentsHub : Hub<IRatioCallback>
    {
        ILogger Log;
        public RatioEnentsHub(ILogger _logger)
        {
            Log = _logger;
        //    var rrr = GlobalHost.ConnectionManager.GetHubContext<RatioEnentsHub>();
        }
        public override Task OnConnectedAsync()
        {
            Log.LogInformation(Context.ConnectionId + "- reconnected");

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Log.LogInformation(Context.ConnectionId + " - Disconnected" +
               ((exception != null) ? "\n" + exception.StackTrace : " OK"));

            return base.OnDisconnectedAsync(exception);
        }
    }


    /// <summary>
    /// Dummy interface for DI 
    /// </summary>
    public interface IRatioEnentsHubFacade : IRatioCallback
    {
    }

    public class RatioEnentsHubFacade : IRatioEnentsHubFacade
    {
        public readonly IHubContext<RatioEnentsHub, IRatioCallback> hubContext;
        public RatioEnentsHubFacade(IHubContext<RatioEnentsHub, IRatioCallback> _hub)
        {
            hubContext = _hub;
           // var rrr = GlobalHost.ConnectionManager.GetHubContext<RatioEnentsHub>();
        }

        async public Task changeRatios(List<CurrencyRatioChange> listCallb)
        {
            if (listCallb.Count > 0)
            {
                await hubContext.Clients.All.changeRatios(listCallb);
            }
           // return listCallb.Count;

        }
    }





}
