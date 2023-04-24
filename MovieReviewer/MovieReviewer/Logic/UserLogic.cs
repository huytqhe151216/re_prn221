using MovieReviewer.Models;

namespace MovieReviewer.Logic
{
    public class UserLogic
    {
        public static User Login(string email, string password)
        {
            User user = null;
            var context = new  MovieReviewerContext();
            user = context.Users.FirstOrDefault(x => x.Email == email&& x.Password==password);
            return user;
        }
    }
}
