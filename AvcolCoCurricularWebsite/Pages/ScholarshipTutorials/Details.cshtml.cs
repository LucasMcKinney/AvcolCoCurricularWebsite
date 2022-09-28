namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

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
}