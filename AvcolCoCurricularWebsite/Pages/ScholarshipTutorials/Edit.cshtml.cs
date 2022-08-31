using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials
{
    public class EditModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public EditModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ScholarshipTutorial ScholarshipTutorial { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScholarshipTutorial = await _context.ScholarshipTutorial
                .Include(s => s.Activity).FirstOrDefaultAsync(m => m.ScholarshipTutorialID == id);

            if (ScholarshipTutorial == null)
            {
                return NotFound();
            }
           ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScholarshipTutorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScholarshipTutorialExists(ScholarshipTutorial.ScholarshipTutorialID))
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

        private bool ScholarshipTutorialExists(int id)
        {
            return _context.ScholarshipTutorial.Any(e => e.ScholarshipTutorialID == id);
        }
    }
}
