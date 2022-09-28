namespace AvcolCoCurricularWebsite.Pages.Sports;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
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
       ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Sport).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SportExists(Sport.SportID))
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

    private bool SportExists(int id)
    {
        return _context.Sport.Any(e => e.SportID == id);
    }
}