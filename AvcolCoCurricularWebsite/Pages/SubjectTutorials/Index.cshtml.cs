using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.SubjectTutorials
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public IList<SubjectTutorial> SubjectTutorial { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            SubjectTutorial = await _context.SubjectTutorial
                .Include(s => s.Activity).ToListAsync();

            var subjecttutorials = from s in _context.SubjectTutorial
                                   select s;

            if (!string.IsNullOrEmpty(SearchString))
            {
                subjecttutorials = subjecttutorials.Where(s => s.Activity.ActivityName.Contains(SearchString));
            }

            SubjectTutorial = await subjecttutorials.ToListAsync();
        }
    }
}