using System;
using System.Collections.Generic;

namespace MovieReviewer.Models
{
    public partial class WatchList
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int? UserId { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual User? User { get; set; }
    }
}
