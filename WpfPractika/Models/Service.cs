using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class Services
    {
        public int IdService { get; set; }
        public string ServiceName { get; set; }
        public decimal ServiceCost { get; set; }
        public string ServiceDest { get; set; }
    }
}
