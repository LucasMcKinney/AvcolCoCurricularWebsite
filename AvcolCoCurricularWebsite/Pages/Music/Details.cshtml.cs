namespace AvcolCoCurricularWebsite.Pages.Music;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public Models.Music Music { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Music = await _context.Music
            .Include(m => m.Activity).FirstOrDefaultAsync(m => m.MusicID == id);

        if (Music == null)
        {
            return NotFound();
        }
        return Page();
    }
}