namespace AvcolCoCurricularWebsite.Pages.Sports;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sport = await _context.Sport.FindAsync(id);

        if (Sport != null)
        {
            _context.Sport.Remove(Sport);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}