namespace AvcolCoCurricularWebsite.Pages.PersonalInformation;

public class DeleteModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DeleteModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.PersonalInformation PersonalInformation { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        PersonalInformation = await _context.PersonalInformation
            .Include(p => p.Staff).FirstOrDefaultAsync(m => m.StaffID == id);

        if (PersonalInformation == null)
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

        PersonalInformation = await _context.PersonalInformation.FindAsync(id);

        if (PersonalInformation != null)
        {
            _context.PersonalInformation.Remove(PersonalInformation);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}