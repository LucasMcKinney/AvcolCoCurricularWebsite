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

namespace AvcolCoCurricularWebsite.Pages.PersonalInformation
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

        public string StaffSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.PersonalInformation> PersonalInformation { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
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

            IQueryable<Models.PersonalInformation> personalinformationIQ = from p in _context.PersonalInformation
                                                                           select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                personalinformationIQ = personalinformationIQ.Where(s => s.Staff.LastName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "staff_desc":
                    personalinformationIQ = personalinformationIQ.OrderByDescending(s => s.Staff.LastName);
                    break;
                default:
                    personalinformationIQ = personalinformationIQ.OrderBy(s => s.Staff.LastName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 10);
            PersonalInformation = await PaginatedList<Models.PersonalInformation>.CreateAsync(personalinformationIQ.Include(p => p.Staff).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}