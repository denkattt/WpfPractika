using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class Subscriptions
    {
        public int IdSubscription { get; set; }
        public string SubscriptionName { get; set; }
        public decimal SubscriptionCost { get; set; }
        public int CountDaySubscription { get; set; }
    }
}
