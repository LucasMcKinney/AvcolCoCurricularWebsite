﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.PerformingArts
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public IList<PerformingArt> PerformingArt { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            PerformingArt = await _context.PerformingArt
                .Include(p => p.Activity).ToListAsync();

            var performingarts = from p in _context.PerformingArt
                                 select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                performingarts = performingarts.Where(s => s.Activity.ActivityName.Contains(SearchString));
            }

            PerformingArt = await performingarts.ToListAsync();
        }
    }
}