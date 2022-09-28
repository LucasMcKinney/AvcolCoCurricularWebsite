namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ScholarshipTutorial ScholarshipTutorial { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ScholarshipTutorial = await _context.ScholarshipTutorial
            .Include(s => s.Activity).FirstOrDefaultAsync(m => m.ScholarshipTutorialID == id);

        if (ScholarshipTutorial == null)
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

        _context.Attach(ScholarshipTutorial).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ScholarshipTutorialExists(ScholarshipTutorial.ScholarshipTutorialID))
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

    private bool ScholarshipTutorialExists(int id)
    {
        return _context.ScholarshipTutorial.Any(e => e.ScholarshipTutorialID == id);
    }
}