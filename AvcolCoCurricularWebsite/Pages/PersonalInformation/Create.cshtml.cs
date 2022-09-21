﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.PersonalInformation
{
    public class CreateModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public CreateModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            return Page();
        }

        [BindProperty]
        public Models.PersonalInformation PersonalInformation { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.

        private readonly DateTime EarliestDate = new DateTime(1922, 01, 01); // the earliest date of birth of a staff member since there are realistically no teachers aged over 100
        private readonly DateTime LatestDate = new DateTime(2001, 01, 01); // the latest date of birth of a staff member since teachers complete their required 4 years of university by at least the age of 21

        public async Task<IActionResult> OnPostAsync()
        {
            Models.Staff staff = (from s in _context.Staff where s.StaffID == PersonalInformation.StaffID select s).FirstOrDefault();
            PersonalInformation.EmailAddress = staff.TeacherCode + "@avcol.school.nz";

            if (!ModelState.IsValid)
            {
                ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
                return Page();
            }
            if (PersonalInformation.DateOfBirth < EarliestDate || PersonalInformation.DateOfBirth > LatestDate)
            {
                ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
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

            Models.PersonalInformation pInfo = (from p in _context.PersonalInformation where p.StaffID == PersonalInformation.StaffID select p).FirstOrDefault();

            if (pInfo != null)
            {
                ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
                ModelState.AddModelError("Custom", "This Staff already has a record of their personal information. Please edit the existing record.");
                return Page();
            }
            else
            {
                _context.PersonalInformation.Add(PersonalInformation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}