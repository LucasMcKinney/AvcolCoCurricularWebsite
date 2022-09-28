namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials;

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

    public PaginatedList<ScholarshipTutorial> ScholarshipTutorials { get; set; }

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

        IQueryable<ScholarshipTutorial> scholarshiptutorialsIQ = from s in _context.ScholarshipTutorial
                                                  select s;

        if (!string.IsNullOrEmpty(searchString))
        {
            scholarshiptutorialsIQ = scholarshiptutorialsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
        }

        scholarshiptutorialsIQ = sortOrder switch
        {
            "activity_desc" => scholarshiptutorialsIQ.OrderByDescending(s => s.Activity.ActivityName),
            _ => scholarshiptutorialsIQ.OrderBy(s => s.Activity.ActivityName),
        };

        var pageSize = Configuration.GetValue("PageSize", 10);
        ScholarshipTutorials = await PaginatedList<ScholarshipTutorial>.CreateAsync(scholarshiptutorialsIQ.Include(s => s.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}