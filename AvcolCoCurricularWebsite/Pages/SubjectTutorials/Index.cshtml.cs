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

        public string ActivitySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<SubjectTutorial> SubjectTutorial { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            ActivitySort = sortOrder == "Activity" ? "activity_desc" : "Activity";
            CurrentFilter = searchString;

            IQueryable<SubjectTutorial> subjecttutorialsIQ = from s in _context.SubjectTutorial
                                                             select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                subjecttutorialsIQ = subjecttutorialsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "activity_desc":
                    subjecttutorialsIQ = subjecttutorialsIQ.OrderByDescending(s => s.Activity.ActivityName);
                    break;
                default:
                    subjecttutorialsIQ = subjecttutorialsIQ.OrderBy(s => s.Activity.ActivityName);
                    break;
            }

            SubjectTutorial = await subjecttutorialsIQ.Include(s => s.Activity).AsNoTracking().ToListAsync();
        }
    }
}