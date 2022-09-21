﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.SubjectTutorials
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
        ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            return Page();
        }

        [BindProperty]
        public SubjectTutorial SubjectTutorial { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SubjectTutorial.Add(SubjectTutorial);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}