using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieReviewer.Models;

namespace MovieReviewer.Pages.Admins.Actors
{
    public class CreateModel : PageModel
    {
        IWebHostEnvironment _env;
        private readonly MovieReviewer.Models.MovieReviewerContext _context;

        public CreateModel(MovieReviewer.Models.MovieReviewerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Actor Actor { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Actors == null || Actor == null)
            {
                return Page();
            }
            //if (Actor.Img != null)
            //{
            //    string imageName = Guid.NewGuid().ToString() + Path.GetExtension(Actor.Img);
            //    var imagePath = Path.Combine(_env.WebRootPath, "movie", "images", imageName);
            //    if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
            //    {
            //        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
            //    }
            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        Actor.Img.CopyTo(stream);
            //    }
            //}
            _context.Actors.Add(Actor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
