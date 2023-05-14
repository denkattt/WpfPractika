using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class SaleBookings
    {
        public int IdSaleBooking { get; set; }
        public int BookingId { get; set; }
        public int SaleId { get; set; }
    }
}
