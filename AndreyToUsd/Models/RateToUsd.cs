using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreyToUsd.Models
{

    public partial class RateToUsd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public  string Code { get; set; }
        public  string Name { get; set; }
        public  double Rate { get; set; }
        public  double Bid { get; set; }
        public   double Ask { get; set; }
        public   DateTime Stored { get; set; }
        public   DateTime LastRefreshed { get; set; }

    }

    public class FromTo
    {
        public RateToUsd From { get; set; }
        public RateToUsd To { get; set; }

        public double Ratio =>
            (From != null & To != null && To.Rate != 0) ? From.Rate / To.Rate : 0;


    }

}


