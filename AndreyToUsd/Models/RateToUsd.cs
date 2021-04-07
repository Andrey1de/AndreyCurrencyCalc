using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreyToUsd.Models
{

    public partial class RateToUsd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public  string code { get; set; }
        public  string name { get; set; }
        public  double rate { get; set; }
        public  double bid { get; set; }
        public   double ask { get; set; }
        public   DateTime stored { get; set; }
        public   DateTime lastRefreshed { get; set; }

    }
}


