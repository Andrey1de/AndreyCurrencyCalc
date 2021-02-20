using AndreyCurrenclyShared.Models;
using AndreyCurrenclyShared.Text;
using System;

namespace AndreyCurrenclyShared.Models

{
    /// <summary>
    /// Class to be used in WebApi requests transport
    /// Cause names  are lower cased
    /// </summary>
    public interface ICurrencyRatioADO
    {
        string pair { get; set; }
        double ratio { get; set; }
        double oldRatio { get; set; }

        DateTime updated { get; set; }

        int status { get; set; }

        bool IsValid();

        CurrencyRatioADO Clone();
    }
    public class CurrencyRatioADO : ICurrencyRatioADO 
    {
  

         public string pair { get; set; }
      
        public double ratio { get; set; } = -1;
       
        public double oldRatio { get; set; } = -1;

        public DateTime updated { get; set; } = new DateTime(1800, 1, 1);
        public int status { get; set; } = 0;

        public bool IsValid()
        {
            return !pair.IsZ() && ratio > 0 && status > 0;
        }

        public CurrencyRatioADO Clone()
        {
            return this.MemberwiseClone() as CurrencyRatioADO;
        }
}
}
