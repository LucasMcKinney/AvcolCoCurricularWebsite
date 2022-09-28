namespace AvcolCoCurricularWebsite.Pages.SubjectTutorials;

public class IndexModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;
    private readonly IConfiguration Configuration;

    public IndexModel(AvcolCoCurricularWebsiteContext context, IConfiguration configuration)
    {
        _context = context;
        Configuration = configuration;
    }

    public string ActivitySort { get; set; }
    public string CurrentFilter { get; set; }
    public string CurrentSort { get; set; }

    public PaginatedList<SubjectTutorial> SubjectTutorials { get; set; }

    public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        ActivitySort = sortOrder == "Activity" ? "activity_desc" : "Activity";

        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        IQueryable<SubjectTutorial> subjecttutorialsIQ = from s in _context.SubjectTutorial
                                                         select s;

        if (!string.IsNullOrEmpty(searchString))
        {
            subjecttutorialsIQ = subjecttutorialsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
        }

        subjecttutorialsIQ = sortOrder switch
        {
            "activity_desc" => subjecttutorialsIQ.OrderByDescending(s => s.Activity.ActivityName),
            _ => subjecttutorialsIQ.OrderBy(s => s.Activity.ActivityName),
        };

        var pageSize = Configuration.GetValue("PageSize", 10);
        SubjectTutorials = await PaginatedList<SubjectTutorial>.CreateAsync(subjecttutorialsIQ.Include(s => s.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}