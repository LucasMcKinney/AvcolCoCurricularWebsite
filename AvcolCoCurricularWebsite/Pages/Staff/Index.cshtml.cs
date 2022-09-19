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

namespace AvcolCoCurricularWebsite.Pages.Staff
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

        public string LastNameSort { get; set; }
        public string FirstNameSort { get; set; }
        public string HireDateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.Staff> Staff { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            LastNameSort = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            FirstNameSort = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            HireDateSort = sortOrder == "HireDate" ? "hiredate_desc" : "HireDate";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

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

            var pageSize = Configuration.GetValue("PageSize", 10);
            Staff = await PaginatedList<Models.Staff>.CreateAsync(staffIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}