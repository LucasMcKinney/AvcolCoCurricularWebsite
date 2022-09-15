using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Music
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

        public IList<Models.Music> Music { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            ActivitySort = sortOrder == "Activity" ? "activity_desc" : "Activity";
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

            Music = await musicIQ.Include(m => m.Activity).AsNoTracking().ToListAsync();
        }
    }
}