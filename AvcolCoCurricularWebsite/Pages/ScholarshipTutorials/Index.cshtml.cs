using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string ActivitySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<ScholarshipTutorial> ScholarshipTutorials { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            ActivitySort = sortOrder == "Activity" ? "activity_desc" : "Activity";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

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

            var pageSize = Configuration.GetValue("PageSize", 10);
            ScholarshipTutorials = await PaginatedList<ScholarshipTutorial>.CreateAsync(scholarshiptutorialsIQ.Include(s => s.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}