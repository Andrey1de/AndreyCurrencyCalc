using AndreyCurrenclyShared.Models;
using AndreyCurrencyBL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreyCurrencyBL.TimerFeatures
{
    public static class ChangeRatioSimulatorMasnager
    {
        /// <summary>
        /// Generates new ratio in randomal chosen member
        /// </summary>
        /// <returns></returns>
        public static List<CurrencyRatioCallback> GetData()
        {
            var r = new Random();
            List<PairsGetTime> list = CentralBLService.AllData;
            List<CurrencyRatioCallback> ret = new List<CurrencyRatioCallback>();
            if(list.Count > 0)
            {
                int num = r.Next(list.Count);
                var ado = list[num].Ratio;
                var koefNew =  1 + ((r.NextDouble() - 0.5) / 5.0);// Change +/- 10% randomaly;

                var newRatio = ado.ratio *= koefNew; // ratio +/- 10% randomaly;

                CurrencyRatioCallback callb = CurrencyRatioCallback.NewCallback(ado, newRatio);
                ret.Add(callb);
             }



            return ret;
        }
    }
}
