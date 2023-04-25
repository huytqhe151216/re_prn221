using MovieReviewer.Models;

namespace MovieReviewer.Logic
{
    public class GenreLogic
    {
        public static List<Genre> GetALlGenres()
        {
            var context = new MovieReviewerContext();
            return context.Genres.ToList();
        }
    }
}
