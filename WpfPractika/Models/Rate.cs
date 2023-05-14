using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class Rates
    {

        public int IdRate { get; set; }
        public string RateName { get; set; }
        public decimal RateCost { get; set; }
        public string RateDest { get; set; }
    }
}
