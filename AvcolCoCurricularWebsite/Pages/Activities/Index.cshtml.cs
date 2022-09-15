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

namespace AvcolCoCurricularWebsite.Pages.Activities
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

        public string ActivityNameSort { get; set; }
        public string StaffSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Activity> Activities { get; set; }
        
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            ActivityNameSort = sortOrder == "ActivityName" ? "activityname_desc" : "ActivityName";
            StaffSort = sortOrder == "Staff" ? "staff_desc" : "Staff";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

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

            var pageSize = Configuration.GetValue("PageSize", 10);
            Activities = await PaginatedList<Activity>.CreateAsync(activitiesIQ.Include(a => a.Staff).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}