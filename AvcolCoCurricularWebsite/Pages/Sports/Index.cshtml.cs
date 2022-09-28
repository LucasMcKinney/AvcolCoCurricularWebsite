namespace AvcolCoCurricularWebsite.Pages.Sports;

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

    public PaginatedList<Sport> Sports { get; set; }

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

        IQueryable<Sport> sportsIQ = from s in _context.Sport
                                     select s;

        if (!string.IsNullOrEmpty(searchString))
        {
            sportsIQ = sportsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
        }

        sportsIQ = sortOrder switch
        {
            "activity_desc" => sportsIQ.OrderByDescending(s => s.Activity.ActivityName),
            _ => sportsIQ.OrderBy(s => s.Activity.ActivityName),
        };

        var pageSize = Configuration.GetValue("PageSize", 10);
        Sports = await PaginatedList<Sport>.CreateAsync(sportsIQ.Include(s => s.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}