namespace AvcolCoCurricularWebsite.Pages.Music;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Music = await _context.Music.FindAsync(id);

        if (Music != null)
        {
            _context.Music.Remove(Music);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}