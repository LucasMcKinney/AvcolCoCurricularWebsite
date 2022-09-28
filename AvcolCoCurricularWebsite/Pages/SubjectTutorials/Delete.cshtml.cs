namespace AvcolCoCurricularWebsite.Pages.SubjectTutorials;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
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
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        SubjectTutorial = await _context.SubjectTutorial.FindAsync(id);

        if (SubjectTutorial != null)
        {
            _context.SubjectTutorial.Remove(SubjectTutorial);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}