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
    public class IndexModel : PageModel
    {
        private readonly MovieReviewer.Models.MovieReviewerContext _context;

        public IndexModel(MovieReviewer.Models.MovieReviewerContext context)
        {
            _context = context;
        }

        public IList<Actor> Actor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Actors != null)
            {
                Actor = await _context.Actors.ToListAsync();
            }
        }
    }
}
