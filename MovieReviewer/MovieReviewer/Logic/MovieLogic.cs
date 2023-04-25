using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
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
        public static void Rate(int userid,int rateStar, string content,int movieId)
        {
            var context = new MovieReviewerContext();
            Rate rate = new Rate();
            rate.UserId = userid;
            rate.MovieId = movieId;
            rate.RateStar= rateStar;
            rate.RateContent= content;
            context.Add(rate);
            context.SaveChanges();
        }
        public static bool CheckRate(int userId,int movieId)
        {
            var context = new MovieReviewerContext();
            Rate rate = null;
            rate = context.Rates.FirstOrDefault(x=>x.UserId==userId && x.MovieId==movieId);
            if (rate!=null)
            {
                return true;
            }
            return false;
        }
        public static void UpdateRate(int userid, int rateStar, string content, int movieId)
        {
            var context = new MovieReviewerContext();
            Rate rate = GetRate(userid, movieId);
            rate.UserId = userid;
            rate.MovieId = movieId;
            rate.RateStar = rateStar;
            rate.RateContent = content;
            context.Update(rate);
            context.SaveChanges();
        }
        public static Rate GetRate(int userId, int movieId)
        {
            var context = new MovieReviewerContext();
            Rate rate  = context.Rates.FirstOrDefault(x => x.UserId == userId && x.MovieId == movieId);
            return rate;
        }
        public static List<Rate> GetListRateOfMovie(int movieId)
        {
            var context = new MovieReviewerContext();

            //return context.Rates.Include(x => x.User).Where(x => x.MovieId == movieId).ToList().OrderBy(x=>x.RateDate);
            return context.Rates.Include(x=>x.User).OrderBy(x => x.RateDate).Where(x=>x.MovieId==movieId).ToList();
            //return context.Rates.Where(x=>x.MovieId==movieId).ToList();
        }
        public static List<FavouriteList> GetFavouriteIdListById(int id)
        {
            var context = new MovieReviewerContext();
            //return context.FavouriteLists.Where(x=>x.UserId== id).ToList(); 
            return context.FavouriteLists.Include(x=>x.Movie).Where(x=>x.UserId==id).ToList();
            
        }
        public static List<Movie> GetListFavouriteMovie(int id)
        {
            List<Movie> list = new List<Movie>();
            foreach (var item in GetFavouriteIdListById(id))
            {
                list.Add(GetMovieDetail((int)item.MovieId));
            }
            return list;
        }
    }
}
