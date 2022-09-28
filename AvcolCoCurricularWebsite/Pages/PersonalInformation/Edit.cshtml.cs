namespace AvcolCoCurricularWebsite.Pages.PersonalInformation;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.PersonalInformation PersonalInformation { get; set; }
    public string DateOfBirthErrorMessage { get; set; }
    public string PhoneNumberErrorMessage { get; set; }
    public string ECPhoneNumberErrorMessage { get; set; }

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
        ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
        return Page();
    }

    private readonly DateTime EarliestDate = new(1922, 01, 01); // the earliest date of birth of a staff member since there are realistically no teachers aged over 100
    private readonly DateTime LatestDate = new(2001, 01, 01); // the latest date of birth of a staff member since teachers complete their required 4 years of university by at least the age of 21

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (PersonalInformation.DateOfBirth < EarliestDate || PersonalInformation.DateOfBirth > LatestDate)
        {
            DateOfBirthErrorMessage = "Invalid Date of Birth.";
            return Page();
        }
        if (PersonalInformation.PhoneNumber.Length != 10)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            PhoneNumberErrorMessage = "Invalid Phone Number.";
            return Page();
        }
        if (PersonalInformation.ECPhoneNumber.Length != 10)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            ECPhoneNumberErrorMessage = "Invalid Emergency Contact Phone Number.";
            return Page();
        }

        _context.Attach(PersonalInformation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonalInformationExists(PersonalInformation.StaffID))
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

    private bool PersonalInformationExists(int id)
    {
        return _context.PersonalInformation.Any(e => e.StaffID == id);
    }
}