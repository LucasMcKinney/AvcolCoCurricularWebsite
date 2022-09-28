namespace AvcolCoCurricularWebsite.Pages.Clubs;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
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
       ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Club).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClubExists(Club.ClubID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool ClubExists(int id)
    {
        return _context.Club.Any(e => e.ClubID == id);
    }
}