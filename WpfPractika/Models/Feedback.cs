using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class Feedbacks
    {
        public int IdFeedback { get; set; }
        public string FeedbackText { get; set; }
        public int FeedbackStars { get; set; }
        public int UserId { get; set; }
    }
}
