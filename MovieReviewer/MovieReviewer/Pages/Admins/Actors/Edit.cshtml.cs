using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Models;

namespace MovieReviewer.Pages.Admins.Actors
{
    public class EditModel : PageModel
    {
        private readonly MovieReviewer.Models.MovieReviewerContext _context;

        public EditModel(MovieReviewer.Models.MovieReviewerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actor Actor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor =  await _context.Actors.FirstOrDefaultAsync(m => m.ActorId == id);
            if (actor == null)
            {
                return NotFound();
            }
            Actor = actor;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var existingActor = _context.Actors.Find(Actor.ActorId);

            if (existingActor != null)
            {
                // Update the properties of the existing actor entity
                existingActor.Name = Actor.Name;
                existingActor.Gender = Actor.Gender;
                existingActor.Img = Actor.Img;
                existingActor.Nationality = Actor.Nationality;
                existingActor.Detail = Actor.Detail;

                // Mark the existing actor entity as modified
                _context.Entry(existingActor).State = EntityState.Modified;
            }
            else
            {
                // Attach the new actor entity to the context
                _context.Actors.Attach(Actor);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool ActorExists(int id)
        {
          return (_context.Actors?.Any(e => e.ActorId == id)).GetValueOrDefault();
        }
    }
}
