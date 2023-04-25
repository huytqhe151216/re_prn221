using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Models;
using NuGet.Packaging;

namespace MovieReviewer.Pages.Admins.Movies
{
	public class CreateModel : PageModel
	{
		private readonly MovieReviewer.Models.MovieReviewerContext _context;

		public CreateModel(MovieReviewer.Models.MovieReviewerContext context)
		{
			_context = context;
		}

		public List<Genre> Genres { get; set; }

		public List<Actor> Actors { get; set; }

		public IActionResult OnGet()
		{
			Genres = _context.Genres.ToList();
			Actors = _context.Actors.ToList();
			return Page();
		}

		[BindProperty]
		public Movie Movie { get; set; } = default!;


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync(int[] genres, int[] actors)
		{
			if (!ModelState.IsValid || _context.Movies == null || Movie == null)
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
			Movie.Genres.AddRange(listGenres);
			Movie.Actors.AddRange(listActors);
			_context.Movies.Add(Movie);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
