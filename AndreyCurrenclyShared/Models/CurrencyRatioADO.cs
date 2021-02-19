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
        double oldratio { get; set; }

        DateTime updated { get; set; }

        int status { get; set; }

        public bool IsValid { get; }
    }
    public class CurrencyRatioADO : ICurrencyRatioADO
    {
      

        public string pair { get; set; }
      
        public double ratio { get; set; } = -1;
       
        public double oldratio { get; set; } = -1;

        public DateTime updated { get; set; } = new DateTime(1800, 1, 1);
        public int status { get; set; } = 0;

        public bool IsValid => !pair.IsZ() && ratio > 0 && status > 0;

    }
}
