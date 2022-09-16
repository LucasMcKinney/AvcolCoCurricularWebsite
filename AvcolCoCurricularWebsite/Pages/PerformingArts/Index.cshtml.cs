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

namespace AvcolCoCurricularWebsite.Pages.PerformingArts
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

        public PaginatedList<PerformingArt> PerformingArts { get; set; }

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

            IQueryable<PerformingArt> performingartsIQ = from p in _context.PerformingArt
                                                         select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                performingartsIQ = performingartsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "activity_desc":
                    performingartsIQ = performingartsIQ.OrderByDescending(s => s.Activity.ActivityName);
                    break;
                default:
                    performingartsIQ = performingartsIQ.OrderBy(s => s.Activity.ActivityName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 10);
            PerformingArts = await PaginatedList<PerformingArt>.CreateAsync(performingartsIQ.Include(p => p.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}