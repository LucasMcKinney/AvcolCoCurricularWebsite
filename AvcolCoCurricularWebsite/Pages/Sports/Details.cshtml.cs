namespace AvcolCoCurricularWebsite.Pages.Sports;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public Sport Sport { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sport = await _context.Sport
            .Include(s => s.Activity).FirstOrDefaultAsync(m => m.SportID == id);

        if (Sport == null)
        {
            return NotFound();
        }
        return Page();
    }
}