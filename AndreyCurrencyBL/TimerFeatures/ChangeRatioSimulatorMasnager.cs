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
        public static List<CurrencyRatioADO> GetChanges()
        {
            var r = new Random();
            List<PairsGetTime> list = CentralBLService.AllData;
            List<CurrencyRatioADO> ret = new List<CurrencyRatioADO>();
            if (list.Count > 0)
            {
                int num = r.Next(0, list.Count);

                var  adoOrig = list[num].Ratio;

                var ado = adoOrig.Clone();
                // var ado = list[num].Ratio;
                var koefNew = 1 + ((r.NextDouble() - 0.5) / 2.5);// Change +/- 20% randomaly;
                ado.oldRatio = ado.ratio;
                ado.ratio *= koefNew; // ratio +/- 10% randomaly;
                ado.status = 2;

                ado.ratio = double.Parse(ado.ratio.ToString("G6"));

                ret.Add(ado);
            }



            return ret;
        }
    }
}
