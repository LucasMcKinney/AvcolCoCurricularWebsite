namespace AvcolCoCurricularWebsite.Pages.SubjectTutorials;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SubjectTutorial SubjectTutorial { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        SubjectTutorial = await _context.SubjectTutorial
            .Include(s => s.Activity).FirstOrDefaultAsync(m => m.SubjectTutorialID == id);

        if (SubjectTutorial == null)
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

        _context.Attach(SubjectTutorial).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubjectTutorialExists(SubjectTutorial.SubjectTutorialID))
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

    private bool SubjectTutorialExists(int id)
    {
        return _context.SubjectTutorial.Any(e => e.SubjectTutorialID == id);
    }
}