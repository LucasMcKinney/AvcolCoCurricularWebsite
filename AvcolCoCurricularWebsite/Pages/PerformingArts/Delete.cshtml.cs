namespace AvcolCoCurricularWebsite.Pages.PerformingArts;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        PerformingArt = await _context.PerformingArt.FindAsync(id);

        if (PerformingArt != null)
        {
            _context.PerformingArt.Remove(PerformingArt);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}