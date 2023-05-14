using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class ServiceOfBookings
    {
        public int IdServiceOfBooking { get; set; }
        public int ServiceId { get; set; }
        public int BookingId { get; set; }
    }
}
