namespace AvcolCoCurricularWebsite.Pages.PerformingArts;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
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
       ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(PerformingArt).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PerformingArtExists(PerformingArt.PerformingArtID))
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

    private bool PerformingArtExists(int id)
    {
        return _context.PerformingArt.Any(e => e.PerformingArtID == id);
    }
}