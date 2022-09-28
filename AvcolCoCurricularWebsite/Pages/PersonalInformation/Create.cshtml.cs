namespace AvcolCoCurricularWebsite.Pages.PersonalInformation;

public class CreateModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public CreateModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
        return Page();
    }

    [BindProperty]
    public Models.PersonalInformation PersonalInformation { get; set; }
    public string DateOfBirthErrorMessage { get; set; }
    public string PhoneNumberErrorMessage { get; set; }
    public string ECPhoneNumberErrorMessage { get; set; }
    public string StaffErrorMessage { get; set; }

    private readonly DateTime EarliestDate = new DateTime(1922, 01, 01); // the earliest date of birth of a staff member since there are realistically no teachers aged over 100
    private readonly DateTime LatestDate = new DateTime(2001, 01, 01); // the latest date of birth of a staff member since teachers complete their required 4 years of university by at least the age of 21

    public async Task<IActionResult> OnPostAsync()
    {
        Models.Staff staff = (from s in _context.Staff where s.StaffID == PersonalInformation.StaffID select s).FirstOrDefault();
        PersonalInformation.EmailAddress = staff.TeacherCode + "@avcol.school.nz";

        if (!ModelState.IsValid)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            return Page();
        }
        if (PersonalInformation.DateOfBirth < EarliestDate || PersonalInformation.DateOfBirth > LatestDate)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
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

        Models.PersonalInformation pInfo = (from p in _context.PersonalInformation where p.StaffID == PersonalInformation.StaffID select p).FirstOrDefault();

        if (pInfo != null)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            StaffErrorMessage = "This Staff already has a record of their Personal Information. Please edit the existing record.";
            return Page();
        }
        else
        {
            _context.PersonalInformation.Add(PersonalInformation);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}