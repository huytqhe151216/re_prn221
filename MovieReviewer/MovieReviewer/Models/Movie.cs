using System;
using System.Collections.Generic;

namespace MovieReviewer.Models
{
    public partial class Movie
    {
        public Movie()
        {
            FavouriteLists = new HashSet<FavouriteList>();
            Rates = new HashSet<Rate>();
            WatchLists = new HashSet<WatchList>();
            Actors = new HashSet<Actor>();
            Genres = new HashSet<Genre>();
        }

        public int MovieId { get; set; }
        public string MovieName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Trailer { get; set; } = null!;
        public string Poster { get; set; } = null!;
        public int Duration { get; set; }
        public string? OriginalLanguage { get; set; }
        public DateTime? DateCreate { get; set; }

        public virtual MovieImg? MovieImg { get; set; }
        public virtual ICollection<FavouriteList> FavouriteLists { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<WatchList> WatchLists { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
