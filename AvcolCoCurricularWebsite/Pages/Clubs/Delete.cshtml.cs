namespace AvcolCoCurricularWebsite.Pages.Clubs;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Club = await _context.Club.FindAsync(id);

        if (Club != null)
        {
            _context.Club.Remove(Club);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}