using AndreyCurrenclyShared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndreyCurrencyBL.Models
{
    public class RatioEnentsModel
    {
        string Pair { get; set; }
        double Ratio { get; set; }
        double Oldratio { get; set; }
   
        DateTime Updated { get; set; }

        public static RatioEnentsModel NewRatio(CurrencyRatioADO ratioAdo
            , double newratio)
        {
            var ret = new RatioEnentsModel()
            {
                Pair = ratioAdo.pair,
                Oldratio = ratioAdo.ratio,
                Ratio = (ratioAdo.ratio = newratio),
                Updated = DateTime.Now

            };
            return ret;

        }

    }
}
   
