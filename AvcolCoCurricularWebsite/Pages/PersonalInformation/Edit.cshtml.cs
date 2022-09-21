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

namespace AvcolCoCurricularWebsite.Pages.PersonalInformation
{
    public class EditModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public EditModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.PersonalInformation PersonalInformation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonalInformation = await _context.PersonalInformation
                .Include(p => p.Staff).FirstOrDefaultAsync(m => m.StaffID == id);

            if (PersonalInformation == null)
            {
                return NotFound();
            }
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.

        private readonly DateTime EarliestDate = new DateTime(1922, 01, 01); // the earliest date of birth of a staff member since there are realistically no teachers aged over 100
        private readonly DateTime LatestDate = new DateTime(2001, 01, 01); // the latest date of birth of a staff member since teachers complete their required 4 years of university by at least the age of 21

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (PersonalInformation.DateOfBirth < EarliestDate || PersonalInformation.DateOfBirth > LatestDate)
            {
                ModelState.AddModelError("Custom", "Invalid Date of Birth.");
                return Page();
            }
            if (PersonalInformation.PhoneNumber.Length != 10)
            {
                ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
                ModelState.AddModelError("Custom", "Invalid Phone Number.");
                return Page();
            }
            if (PersonalInformation.ECPhoneNumber.Length != 10)
            {
                ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
                ModelState.AddModelError("Custom", "Invalid Emergency Contact Phone Number.");
                return Page();
            }

            _context.Attach(PersonalInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalInformationExists(PersonalInformation.StaffID))
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

        private bool PersonalInformationExists(int id)
        {
            return _context.PersonalInformation.Any(e => e.StaffID == id);
        }
    }
}