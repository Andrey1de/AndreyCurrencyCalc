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
        public static List<CurrencyRatioChange> GetChanges()
        {
            var r = new Random();
            List<PairsGetTime> list = CentralBLService.AllData;
            List<CurrencyRatioChange> ret = new List<CurrencyRatioChange>();
            if(list.Count > 0)
            {
                int num = r.Next(0,list.Count);
                var ado = list[num].Ratio;
                var koefNew =  1 + ((r.NextDouble() - 0.5) / 2.5);// Change +/- 20% randomaly;

                var newRatio = ado.ratio *= koefNew; // ratio +/- 10% randomaly;

                CurrencyRatioChange callb = CurrencyRatioChange.NewCallback(ado, newRatio);
                ret.Add(callb);
             }



            return ret;
        }
    }
}
