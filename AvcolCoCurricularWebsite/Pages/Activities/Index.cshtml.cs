using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Activities
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Activity> Activity { get;set; }
        [BindProperty(SupportsGet = true)]
        
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            FNameSort = sortOrder == "FName" ? "FName_desc" : "FName";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentFilter = searchString;

            IQueryable<Activity> activitiesIQ = from a in _context.Activity
                                                select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                activitiesIQ = activitiesIQ.Where(s => s.ActivityName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    activitiesIQ = activitiesIQ.OrderByDescending(s => s.ActivityName);
                    break;
                default:
                    activitiesIQ = activitiesIQ.OrderBy(s => s.ActivityName);
                    break;
            }
            
            //REWRITE IQ SWITCH

            Activity = await activitiesIQ.AsNoTracking().ToListAsync();
        }
    }
}