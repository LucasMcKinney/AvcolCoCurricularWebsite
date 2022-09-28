namespace AvcolCoCurricularWebsite.Pages.Staff;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Staff Staff { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Staff = await _context.Staff.FirstOrDefaultAsync(m => m.StaffID == id);

        if (Staff == null)
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

        Staff = await _context.Staff.FindAsync(id);

        if (Staff != null)
        {
            _context.Staff.Remove(Staff);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}