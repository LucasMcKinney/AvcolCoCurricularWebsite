namespace AvcolCoCurricularWebsite.Pages.Clubs;

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

    public PaginatedList<Club> Clubs { get; set; }

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

        IQueryable<Club> clubsIQ = from c in _context.Club
                                   select c;

        if (!string.IsNullOrEmpty(searchString))
        {
            clubsIQ = clubsIQ.Where(s => s.Activity.ActivityName.ToUpper().Contains(searchString.ToUpper()));
        }

        clubsIQ = sortOrder switch
        {
            "activity_desc" => clubsIQ.OrderByDescending(s => s.Activity.ActivityName),
            _ => clubsIQ.OrderBy(s => s.Activity.ActivityName),
        };

        var pageSize = Configuration.GetValue("PageSize", 10);
        Clubs = await PaginatedList<Club>.CreateAsync(clubsIQ.Include(c => c.Activity).AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}