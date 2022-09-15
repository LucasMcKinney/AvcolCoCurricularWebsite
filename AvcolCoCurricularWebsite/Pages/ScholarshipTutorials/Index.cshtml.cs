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

        public string ActivitySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<ScholarshipTutorial> ScholarshipTutorial { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            ActivitySort = sortOrder == "Activity" ? "activity_desc" : "Activity";
            CurrentFilter = searchString;

            IQueryable<ScholarshipTutorial> scholarshiptutorialsIQ = from s in _context.ScholarshipTutorial
                                                      select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                scholarshiptutorialsIQ = scholarshiptutorialsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "activity_desc":
                    scholarshiptutorialsIQ = scholarshiptutorialsIQ.OrderByDescending(s => s.Activity.ActivityName);
                    break;
                default:
                    scholarshiptutorialsIQ = scholarshiptutorialsIQ.OrderBy(s => s.Activity.ActivityName);
                    break;
            }

            ScholarshipTutorial = await scholarshiptutorialsIQ.Include(s => s.Activity).AsNoTracking().ToListAsync();
        }
    }
}