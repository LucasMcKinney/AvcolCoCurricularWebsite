using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.PersonalInformation
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public string StaffSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Models.PersonalInformation> PersonalInformation { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            StaffSort = sortOrder == "Staff" ? "staff_desc" : "Staff";
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

            PersonalInformation = await personalinformationIQ.Include(p => p.Staff).AsNoTracking().ToListAsync();
        }
    }
}