namespace AvcolCoCurricularWebsite.Pages.Sports;

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
    public Sport Sport { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Sport.Add(Sport);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}