namespace AvcolCoCurricularWebsite.Pages.Staff;

public class IndexModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;
    private readonly IConfiguration Configuration;

    public IndexModel(AvcolCoCurricularWebsiteContext context, IConfiguration configuration)
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

        staffIQ = sortOrder switch
        {
            "lastname_desc" => staffIQ.OrderByDescending(s => s.LastName),
            "FirstName" => staffIQ.OrderBy(s => s.FirstName),
            "firstname_desc" => staffIQ.OrderByDescending(s => s.FirstName),
            "HireDate" => staffIQ.OrderBy(s => s.HireDate),
            "hiredate_desc" => staffIQ.OrderByDescending(s => s.HireDate),
            _ => staffIQ.OrderBy(s => s.LastName),
        };

        var pageSize = Configuration.GetValue("PageSize", 10);
        Staff = await PaginatedList<Models.Staff>.CreateAsync(staffIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}