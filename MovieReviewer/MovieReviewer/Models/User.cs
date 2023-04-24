using System;
using System.Collections.Generic;

namespace MovieReviewer.Models
{
    public partial class User
    {
        public User()
        {
            FavouriteLists = new HashSet<FavouriteList>();
            Rates = new HashSet<Rate>();
            WatchLists = new HashSet<WatchList>();
        }

        public int Id { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public string? Img { get; set; }

        public virtual ICollection<FavouriteList> FavouriteLists { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<WatchList> WatchLists { get; set; }
    }
}
