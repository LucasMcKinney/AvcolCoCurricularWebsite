using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Clubs
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

        public IList<Club> Club { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            ActivitySort = sortOrder == "Activity" ? "activity_desc" : "Activity";
            CurrentFilter = searchString;

            IQueryable<Club> clubsIQ = from c in _context.Club
                                       select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                clubsIQ = clubsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "activity_desc":
                    clubsIQ = clubsIQ.OrderByDescending(s => s.Activity.ActivityName);
                    break;
                default:
                    clubsIQ = clubsIQ.OrderBy(s => s.Activity.ActivityName);
                    break;
            }

            Club = await clubsIQ.Include(c => c.Activity).AsNoTracking().ToListAsync();
        }
    }
}