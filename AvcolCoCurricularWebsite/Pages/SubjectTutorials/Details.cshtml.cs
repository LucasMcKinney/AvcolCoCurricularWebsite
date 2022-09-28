namespace AvcolCoCurricularWebsite.Pages.SubjectTutorials;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

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
}