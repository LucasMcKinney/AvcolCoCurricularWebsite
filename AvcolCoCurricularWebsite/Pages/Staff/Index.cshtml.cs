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
            LastNameSort = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            FirstNameSort = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            HireDateSort = sortOrder == "HireDate" ? "hiredate_desc" : "HireDate";
            CurrentFilter = searchString;

            IQueryable<Models.Staff> staffIQ = from s in _context.Staff
                                               select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                staffIQ = staffIQ.Where(s => s.TeacherCode.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "lastname_desc":
                    staffIQ = staffIQ.OrderByDescending(s => s.LastName);
                    break;
                case "FirstName":
                    staffIQ = staffIQ.OrderBy(s => s.FirstName);
                    break;
                case "firstname_desc":
                    staffIQ = staffIQ.OrderByDescending(s => s.FirstName);
                    break;
                case "HireDate":
                    staffIQ = staffIQ.OrderBy(s => s.HireDate);
                    break;
                case "hiredate_desc":
                    staffIQ = staffIQ.OrderByDescending(s => s.HireDate);
                    break;
                default:
                    staffIQ = staffIQ.OrderBy(s => s.LastName);
                    break;
            }

            Staff = await staffIQ.AsNoTracking().ToListAsync();
        }
    }
}