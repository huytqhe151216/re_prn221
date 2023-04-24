using System;
using System.Collections.Generic;

namespace MovieReviewer.Models
{
    public partial class RateLike
    {
        public int RateId { get; set; }
        public int UserId { get; set; }

        public virtual Rate Rate { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
