namespace AvcolCoCurricularWebsite.Pages.Music;

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
    public Models.Music Music { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Music.Add(Music);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}