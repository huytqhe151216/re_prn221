using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Models;

namespace MovieReviewer.Pages.Admins.Actors
{
    public class DetailsModel : PageModel
    {
        private readonly MovieReviewer.Models.MovieReviewerContext _context;

        public DetailsModel(MovieReviewer.Models.MovieReviewerContext context)
        {
            _context = context;
        }

      public Actor Actor { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FirstOrDefaultAsync(m => m.ActorId == id);
            if (actor == null)
            {
                return NotFound();
            }
            else 
            {
                Actor = actor;
            }
            return Page();
        }
    }
}
