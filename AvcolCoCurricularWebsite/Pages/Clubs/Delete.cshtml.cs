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
    public class DeleteModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public DeleteModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club = await _context.Club.FindAsync(id);

            if (Club != null)
            {
                _context.Club.Remove(Club);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
