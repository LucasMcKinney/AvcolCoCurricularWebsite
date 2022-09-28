namespace AvcolCoCurricularWebsite.Pages.Clubs;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public Club Club { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Club = await _context.Club
            .Include(c => c.Activity).FirstOrDefaultAsync(m => m.ClubID == id);

        if (Club == null)
        {
            return NotFound();
        }
        return Page();
    }
}