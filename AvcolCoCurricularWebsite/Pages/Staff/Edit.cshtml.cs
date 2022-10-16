namespace AvcolCoCurricularWebsite.Pages.Staff;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Staff Staff { get; set; }
    public string HireDateErrorMessage { get; set; }

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

    private readonly DateTime BeginningHireDate = new(1945, 01, 01); // set BeginningHireDate to when avondale college started hiring staff

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Staff.HireDate > DateTime.Now || Staff.HireDate < BeginningHireDate)
        {
            HireDateErrorMessage = "Invalid Hire Date.";
            return Page();
        }

        _context.Attach(Staff).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StaffExists(Staff.StaffID))
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

    private bool StaffExists(int id)
    {
        return _context.Staff.Any(e => e.StaffID == id);
    }
}