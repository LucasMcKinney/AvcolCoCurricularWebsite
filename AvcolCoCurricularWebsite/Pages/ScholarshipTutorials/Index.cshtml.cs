using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public IList<ScholarshipTutorial> ScholarshipTutorial { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            ScholarshipTutorial = await _context.ScholarshipTutorial
                .Include(s => s.Activity).ToListAsync();

            var scholarshiptutorials = from s in _context.ScholarshipTutorial
                                       select s;

            if (!string.IsNullOrEmpty(SearchString))
            {
                scholarshiptutorials = scholarshiptutorials.Where(s => s.Activity.ActivityName.Contains(SearchString));
            }

            ScholarshipTutorial = await scholarshiptutorials.ToListAsync();
        }
    }
}