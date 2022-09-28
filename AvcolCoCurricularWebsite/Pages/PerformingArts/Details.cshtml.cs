namespace AvcolCoCurricularWebsite.Pages.PerformingArts;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public PerformingArt PerformingArt { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        PerformingArt = await _context.PerformingArt
            .Include(p => p.Activity).FirstOrDefaultAsync(m => m.PerformingArtID == id);

        if (PerformingArt == null)
        {
            return NotFound();
        }
        return Page();
    }
}