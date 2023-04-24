using System;
using System.Collections.Generic;

namespace MovieReviewer.Models
{
    public partial class MovieImg
    {
        public int MovieId { get; set; }
        public string? Img { get; set; }

        public virtual Movie Movie { get; set; } = null!;
    }
}
