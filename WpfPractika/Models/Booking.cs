using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class Bookings
    {
        public int IdBooking { get; set; }
        public string NumberBooking { get; set; }
        public DateTime DateTimeStartBooking { get; set; }
        public DateTime DateTimeEndBooking { get; set; }
        public int RateId { get; set; }
        public int HallSeatId { get; set; }
        public int UserId { get; set; }
        public int? PcId { get; set; }
        public int? GameConsoleId { get; set; }
    }
}
