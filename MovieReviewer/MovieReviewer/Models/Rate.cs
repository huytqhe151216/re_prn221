using System;
using System.Collections.Generic;

namespace MovieReviewer.Models
{
    public partial class Rate
    {
        public int RateId { get; set; }
        public string? RateContent { get; set; }
        public int RateStar { get; set; }
        public int? UserId { get; set; }
        public int? MovieId { get; set; }
        public DateTime? RateDate { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual User? User { get; set; }
    }
}
