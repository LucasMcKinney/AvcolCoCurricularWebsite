using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Sports
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

        public IList<Sport> Sport { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            ActivitySort = string.IsNullOrEmpty(sortOrder) ? "activity_desc" : "";
            CurrentFilter = searchString;

            IQueryable<Sport> sportsIQ = from s in _context.Sport
                                         select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                sportsIQ = sportsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "activity_desc":
                    sportsIQ = sportsIQ.OrderByDescending(s => s.Activity.ActivityName);
                    break;
                default:
                    sportsIQ = sportsIQ.OrderBy(s => s.Activity.ActivityName);
                    break;
            }

            Sport = await sportsIQ.Include(s => s.Activity).AsNoTracking().ToListAsync();
        }
    }
}