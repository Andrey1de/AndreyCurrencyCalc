using AndreyCurrenclyShared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndreyCurrenclyShared.Models
{
    public class RatioEnentADO
    {
        string pair { get; set; }
        double ratio { get; set; }
        double oldratio { get; set; }
   
        DateTime updated { get; set; }

        public static RatioEnentADO NewRatio(CurrencyRatioADO ratioAdo
            , double newratio)
        {
            var ret = new RatioEnentADO()
            {
                pair = ratioAdo.pair,
                oldratio = ratioAdo.ratio,
                ratio = (ratioAdo.ratio = newratio),
                updated = DateTime.Now

            };
            return ret;

        }

    }
}
   
