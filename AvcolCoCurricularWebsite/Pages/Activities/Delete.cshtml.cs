namespace AvcolCoCurricularWebsite.Pages.Activities;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Activity Activity { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Activity = await _context.Activity
            .Include(a => a.Staff).FirstOrDefaultAsync(m => m.ActivityID == id);

        if (Activity == null)
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

        Activity = await _context.Activity.FindAsync(id);

        if (Activity != null)
        {
            _context.Activity.Remove(Activity);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}