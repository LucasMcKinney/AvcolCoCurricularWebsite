using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Staff
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public IndexModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public string LastNameSort { get; set; }
        public string FirstNameSort { get; set; }
        public string HireDateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Models.Staff> Staff { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            LastNameSort = string.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            FirstNameSort = string.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            HireDateSort = sortOrder == "HireDate" ? "hiredate_desc" : "HireDate";
            CurrentFilter = searchString;

            IQueryable<Models.Staff> staffIQ = from s in _context.Staff
                                               select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                staffIQ = staffIQ.Where(s => s.TeacherCode.ToUpper().Contains(searchString.ToUpper()));
            }

            // CONTINUE HERE

            switch (sortOrder)
            {
                case "lastname_desc":
                    staffIQ = staffIQ.OrderByDescending(s => s.LastName);
                    break;
                default:
                    staffIQ = staffIQ.OrderBy(s => s.Activity);
                    break;
            }

            Staff = await staffIQ.Include(c => c.Activity).AsNoTracking().ToListAsync();
        }
    }
}