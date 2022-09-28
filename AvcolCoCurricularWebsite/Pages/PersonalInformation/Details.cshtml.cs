namespace AvcolCoCurricularWebsite.Pages.PersonalInformation;

public class DetailsModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public DetailsModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

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
}