using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MovieReviewer.Models;

namespace MovieReviewer.Logic
{
    public class MovieLogic
    {
        
        public static Movie GetMovieDetail(int id)
        {
            var context = new MovieReviewerContext();

            return context.Movies.FirstOrDefault(x=>x.MovieId==id);
        }
        public static bool CheckLoveMovie(int userId,int movieId)
        {
            var context = new MovieReviewerContext();
            FavouriteList favourite= context.FavouriteLists.FirstOrDefault(x=>x.MovieId==movieId&& x.UserId==userId);
            if (favourite==null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        public static void InsertFavourite(int userid,int movieId)
        {
            var context = new MovieReviewerContext();
            FavouriteList list = new FavouriteList
            {
                UserId = userid,
                MovieId = movieId
            };
            context.Add(list);
            context.SaveChanges();
        }
        public static void DeleteFavourite(int userid, int movieId)
        {
            var context = new MovieReviewerContext();
            FavouriteList favourite = context.FavouriteLists.FirstOrDefault(x => x.MovieId == movieId && x.UserId == userid);
            context.Remove(favourite);
            context.SaveChanges();
        }
    }
}
