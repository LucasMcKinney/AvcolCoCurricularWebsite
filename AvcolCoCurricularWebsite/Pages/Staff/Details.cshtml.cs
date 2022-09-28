namespace AvcolCoCurricularWebsite.Pages.Staff;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public Models.Staff Staff { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Staff = await _context.Staff.FirstOrDefaultAsync(m => m.StaffID == id);

        if (Staff == null)
        {
            return NotFound();
        }
        return Page();
    }
}