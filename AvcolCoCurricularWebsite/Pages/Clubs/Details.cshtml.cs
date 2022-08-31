using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Clubs
{
    public class DetailsModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public DetailsModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public Club Club { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club = await _context.Club
                .Include(c => c.Activity).FirstOrDefaultAsync(m => m.ClubID == id);

            if (Club == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
