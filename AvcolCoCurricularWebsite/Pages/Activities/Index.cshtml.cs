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

        public string ActivityNameSort { get; set; }
        public string StaffSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Activity> Activity { get;set; }
        
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            ActivityNameSort = sortOrder == "ActivityName" ? "activityname_desc" : "ActivityName";
            StaffSort = sortOrder == "Staff" ? "staff_desc" : "Staff";
            CurrentFilter = searchString;

            IQueryable<Activity> activitiesIQ = from a in _context.Activity
                                                select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                activitiesIQ = activitiesIQ.Where(s => s.ActivityName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "activityname_desc":
                    activitiesIQ = activitiesIQ.OrderByDescending(s => s.ActivityName);
                    break;
                case "Staff":
                    activitiesIQ = activitiesIQ.OrderBy(s => s.Staff.LastName);
                    break;
                case "staff_desc":
                    activitiesIQ = activitiesIQ.OrderByDescending(s => s.Staff.LastName);
                    break;
                default:
                    activitiesIQ = activitiesIQ.OrderBy(s => s.ActivityName);
                    break;
            }

            Activity = await activitiesIQ.Include(a => a.Staff).AsNoTracking().ToListAsync();
        }
    }
}