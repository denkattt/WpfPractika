using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class HistoryBookings
    {
        public int IdHistoryBooking { get; set; }
        public DateTime DateOfChange { get; set; }
        public int BookingId { get; set; }
        public string Operation { get; set; }
        public int IsDeleted { get; set; }
    }
}
