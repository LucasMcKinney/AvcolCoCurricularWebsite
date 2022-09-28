namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
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
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ScholarshipTutorial = await _context.ScholarshipTutorial.FindAsync(id);

        if (ScholarshipTutorial != null)
        {
            _context.ScholarshipTutorial.Remove(ScholarshipTutorial);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}