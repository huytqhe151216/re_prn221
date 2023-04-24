using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewer.Logic;
using MovieReviewer.Models;
using Newtonsoft.Json;

namespace MovieReviewer.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User user { get; set; }

        [BindProperty]
        public string message { get; set; }
        public void OnGet()
        {
            message = "Enter email and password";
        }
        public IActionResult OnPost()
        {
            User user = UserLogic.Login(this.user.Email, this.user.Password);
            if (user!=null)
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                return RedirectToPage("/Index");
            }
            else
            {
                message = "Error Email or password";
                return Page();
            }
           
        }
    }
}
