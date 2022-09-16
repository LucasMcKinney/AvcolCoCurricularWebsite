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

namespace AvcolCoCurricularWebsite.Pages.Music
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

        public PaginatedList<Models.Music> Music { get; set; }

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

            IQueryable<Models.Music> musicIQ = from m in _context.Music
                                               select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                musicIQ = musicIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "activity_desc":
                    musicIQ = musicIQ.OrderByDescending(s => s.Activity.ActivityName);
                    break;
                default:
                    musicIQ = musicIQ.OrderBy(s => s.Activity.ActivityName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 10);
            Music = await PaginatedList<Models.Music>.CreateAsync(musicIQ.Include(m => m.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}