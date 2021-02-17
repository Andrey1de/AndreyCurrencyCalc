using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AndreyCurrencyBL.Services;
using AndreyCurrenclyShared.Models;
using AndreyCurrenclyShared.Text;
using System.Net;
using System.Text;
using AndreyCurrencyBL.TimerFeatures;

namespace AndreyCurrencyBL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyRatiosController : ControllerBase
    {

        private readonly ILogger<CurrencyRatiosController> Logger;
        private readonly ICentralBLService ConvSvc;

        public CurrencyRatiosController(
            ILogger<CurrencyRatiosController> logger,
            ICentralBLService _converter
            )
        {
            Logger = logger;
            ConvSvc = _converter;
        }

        [Route("pair/{from}/{to}")]
        [HttpGet]
        public async Task<ActionResult<CurrencyRatioADO>> ConvertPair(string from, string to)
        {
            return await ConvSvc.GetRatioForPair(from, to);
        }

        [Route("delimited/{delim}")]
        [HttpGet]
        public async Task<ActionResult<CurrencyRatioADO[]>> GetDelimited(
            string delim)
        {
            delim = delim.ToNZ().Replace("%2C", ",").Replace("%2F", "-").ToLower().Trim().TrimEnd(",;".ToCharArray());

            List<FromTo> listFromTo = delim.SplitDelimFromTo("-/").Where(p=>p.IsValid).ToList();
            var ret = new List<CurrencyRatioADO>();
            if (listFromTo.Count == 1)
            {
                CurrencyRatioADO ret1 = await ConvSvc.GetRatioForPair(listFromTo[0].From, listFromTo[0].To);
                if(ret1 != null)
                {
                    ret.Add(ret1);
                }

            }
            else if (listFromTo.Count > 1)
            {
                 ret = await ConvSvc.GetRatioForPairs(listFromTo.ToArray());
    
            }


            return Ok(ret.ToArray());
        }


        [HttpGet]
        [Route("default")]
        public ActionResult<string> GetDefault()
        {
            return Ok(ConvSvc.DefaultCurrencyPairs);
        }

        [HttpGet]
        [Route("convertorName")]
        public async Task<ActionResult<string>> GetConvertorName()
        {
            var ret = await ConvSvc.GetConvertorName();
            return ret;
        }

        [HttpGet]
        [Route("sendevent")]
        public  ActionResult<CurrencyRatioCallback> TrySendEvent()
        {

            List<CurrencyRatioCallback> ret = ChangeRatioSimulatorMasnager.GetData();
            return Ok(ret[0]);
        }
    }
}
