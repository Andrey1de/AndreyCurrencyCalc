using System;
using System.Collections.Generic;
using System.Text;

namespace AndreyCurrenclyShared.Models
{
    public class CurrencyRatioCallback
    {
        public string Pair { get; set; }
        public double OldRatio { get; set; }
        public double Ratio { get; set; }

        public DateTime Updated { get; set; }
        public CurrencyRatioCallback()
        {

        }
        public static CurrencyRatioCallback NewCallback(CurrencyRatioADO ratioAdo
            , double newratio)
        {
            var ret = new CurrencyRatioCallback()
            {
                Pair = ratioAdo.pair,
                OldRatio =  ratioAdo.ratio,
                Ratio = (ratioAdo.ratio = newratio),
                Updated = DateTime.Now

            };

            
            return ret;

        }

    }
    public class CurrencyRatioModel
    {
        public List<CurrencyRatioCallback> Data { get; } 
        public string Label { get; set; }

        public DateTime Created { get;  } 
        public CurrencyRatioModel()
        {
            Data = new List<CurrencyRatioCallback>();
            Created  = DateTime.Now;
        }
    }
}
