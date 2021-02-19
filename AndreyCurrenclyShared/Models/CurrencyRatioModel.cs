using System;
using System.Collections.Generic;
using System.Text;

namespace AndreyCurrenclyShared.Models
{
    public class CurrencyRatioChange
    {
        public string Pair { get; set; }
        public double OldRatio { get; set; }
        public double Ratio { get; set; }

        public DateTime Updated { get; set; }
        public CurrencyRatioChange()
        {

        }
        public static CurrencyRatioChange NewCallback(CurrencyRatioADO ratioAdo
            , double newratio)
        {
            var ret = new CurrencyRatioChange()
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
        public List<CurrencyRatioChange> Data { get; } 
        public string Label { get; set; }

        public DateTime Created { get;  } 
        public CurrencyRatioModel()
        {
            Data = new List<CurrencyRatioChange>();
            Created  = DateTime.Now;
        }
    }
}
