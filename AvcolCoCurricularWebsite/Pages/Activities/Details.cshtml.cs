namespace AvcolCoCurricularWebsite.Pages.Activities;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public Activity Activity { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Activity = await _context.Activity
            .Include(a => a.Staff).FirstOrDefaultAsync(m => m.ActivityID == id);

        if (Activity == null)
        {
            return NotFound();
        }
        return Page();
    }
}