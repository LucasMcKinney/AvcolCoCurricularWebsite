namespace AvcolCoCurricularWebsite.Pages.Music;

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

    public PaginatedList<Models.Music> Music { get; set; }

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

        IQueryable<Models.Music> musicIQ = from m in _context.Music
                                           select m;

        if (!string.IsNullOrEmpty(searchString))
        {
            musicIQ = musicIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
        }

        musicIQ = sortOrder switch
        {
            "activity_desc" => musicIQ.OrderByDescending(s => s.Activity.ActivityName),
            _ => musicIQ.OrderBy(s => s.Activity.ActivityName),
        };

        var pageSize = Configuration.GetValue("PageSize", 10);
        Music = await PaginatedList<Models.Music>.CreateAsync(musicIQ.Include(m => m.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}