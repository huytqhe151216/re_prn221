using System;
using System.Collections.Generic;

namespace MovieReviewer.Models
{
    public partial class Actor
    {
        public Actor()
        {
            Movies = new HashSet<Movie>();
        }

        public int ActorId { get; set; }
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Img { get; set; } = null!;
        public string? Detail { get; set; }
        public string? Nationality { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
