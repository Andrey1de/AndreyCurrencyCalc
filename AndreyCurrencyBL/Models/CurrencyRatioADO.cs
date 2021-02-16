using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndreyCurrencyBL.Models

{
    /// <summary>
    /// Class to be used in WebApi requests transport
    /// Cause names  are camel cased
    /// </summary>
    public interface ICurrencyRatioADO
    {
        string pair { get; set; }
        double ratio { get; set; }

        DateTime updated { get; set; }

        public int state { get; set; }
    }
    public class CurrencyRatioADO : ICurrencyRatioADO
    {
      
        public string pair { get; set; }
        public double ratio { get; set; }
        public DateTime updated { get; set; } = new DateTime(1800, 1, 1);
        public int state { get; set; } = 0;
    }
}
