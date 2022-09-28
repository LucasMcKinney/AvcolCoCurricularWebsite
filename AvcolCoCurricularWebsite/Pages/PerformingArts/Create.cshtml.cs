namespace AvcolCoCurricularWebsite.Pages.PerformingArts;

public class CreateModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public CreateModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
    ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
        return Page();
    }

    [BindProperty]
    public PerformingArt PerformingArt { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.PerformingArt.Add(PerformingArt);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}