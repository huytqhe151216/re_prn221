using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Models;
using NuGet.Packaging;

namespace MovieReviewer.Pages.Admins.Movies
{
    public class EditModel : PageModel
    {
        private readonly MovieReviewer.Models.MovieReviewerContext _context;

        public EditModel(MovieReviewer.Models.MovieReviewerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;
        public List<Genre> Genres { get; set; }

        public List<Actor> Actors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.Include(x => x.Genres).Include(x => x.Actors).FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            Genres = _context.Genres.ToList();
            Actors = _context.Actors.ToList();
            Movie = movie;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] genres, int[] actors)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            List<Genre> listGenres = new();
            foreach (int i in genres)
            {
                listGenres.Add(_context.Genres.FirstOrDefault(x => x.GenreId == i));
            }
            List<Actor> listActors = new();
            foreach (int i in actors)
            {
                listActors.Add(_context.Actors.FirstOrDefault(x => x.ActorId == i));
            }
            // Find the existing entity with the same primary key
            var existingEntity = _context.Movies.Find(Movie.MovieId);

            if (existingEntity != null)
            {
                // Detach the existing entity from the context
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            Movie.Genres = listGenres;
            Movie.Actors = listActors;
            // Attach the new entity to the context
            _context.Entry(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.MovieId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
