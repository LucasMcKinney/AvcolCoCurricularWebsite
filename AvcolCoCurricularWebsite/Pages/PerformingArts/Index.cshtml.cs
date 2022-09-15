using System;
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

        public string ActivitySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<PerformingArt> PerformingArt { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            ActivitySort = sortOrder == "Activity" ? "activity_desc" : "Activity";
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

            PerformingArt = await performingartsIQ.Include(p => p.Activity).AsNoTracking().ToListAsync();
        }
    }
}