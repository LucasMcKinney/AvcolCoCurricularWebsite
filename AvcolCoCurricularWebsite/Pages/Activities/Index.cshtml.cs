namespace AvcolCoCurricularWebsite.Pages.Activities;

public class IndexModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;
    private readonly IConfiguration Configuration;

    public IndexModel(AvcolCoCurricularWebsiteContext context, IConfiguration configuration)
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

        activitiesIQ = sortOrder switch
        {
            "activityname_desc" => activitiesIQ.OrderByDescending(s => s.ActivityName),
            "Staff" => activitiesIQ.OrderBy(s => s.Staff.LastName),
            "staff_desc" => activitiesIQ.OrderByDescending(s => s.Staff.LastName),
            _ => activitiesIQ.OrderBy(s => s.ActivityName),
        };

        var pageSize = Configuration.GetValue("PageSize", 10);
        Activities = await PaginatedList<Activity>.CreateAsync(activitiesIQ.Include(a => a.Staff).AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}