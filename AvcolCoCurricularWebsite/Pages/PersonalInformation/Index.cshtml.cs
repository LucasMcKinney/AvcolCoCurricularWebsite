﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.PersonalInformation
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public IList<Models.PersonalInformation> PersonalInformation { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            PersonalInformation = await _context.PersonalInformation
                .Include(p => p.Staff).ToListAsync();

            var personalinformation = from p in _context.PersonalInformation
                                      select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                personalinformation = personalinformation.Where(s => s.Staff.FullName.Contains(SearchString));
            }

            PersonalInformation = await personalinformation.ToListAsync();
        }
    }
}