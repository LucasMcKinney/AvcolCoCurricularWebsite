namespace AvcolCoCurricularWebsite.Pages.Staff;

public class CreateModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public CreateModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Models.Staff Staff { get; set; }
    public string StaffErrorMessage { get; set; }
    public string LastNameErrorMessage { get; set; }
    public string FirstNameErrorMessage { get; set; }
    public string HireDateErrorMessage { get; set; }

    private readonly DateTime BeginningHireDate = new(1945, 01, 01); // set BeginningHireDate to when avondale college started hiring staff

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var staffName = (from s in _context.Staff
                         where s.FirstName == Staff.FirstName && s.LastName == Staff.LastName
                         select s).FirstOrDefault(); // checks if there are any staff with the same first name and last name which already exists

        if (staffName != null)
        {
            StaffErrorMessage = "This Staff already has a record. Please edit the existing record."; // displays error message
            return Page();
        }
        if (!Staff.LastName.Any(char.IsLetter))
        {
            LastNameErrorMessage = "Invalid Last Name. Last Name must contain letters only."; // displays error message
            return Page();
        }
        if (!Staff.FirstName.Any(char.IsLetter))
        {
            FirstNameErrorMessage = "Invalid First Name. First Name must contain letters only."; // displays error message
            return Page();
        }

        var lastName = Staff.LastName;
        var teacherCode = " ";

        Models.Staff staff = null;

        for (int f = 0; f < Staff.FirstName.Length; f++) // loop through the first name until the end is reached
        {
            var firstLetters = Staff.FirstName.Substring(f, 1).ToUpper(); // first letter of the first name in uppercase

            for (int i = 0; i < lastName.Length - 1; i++) // check every pair of letters in the last name until a unique pair is found
            {
                teacherCode = firstLetters + lastName.Substring(i, 2).ToUpper();

                staff = (from s in _context.Staff
                         where s.TeacherCode == teacherCode
                         select s).FirstOrDefault();

                if (staff == null) // if there is not a staff with the same name in the database then do not take the next pair of letters in the last name
                {
                    break;
                }
            }

            if (staff == null) // if there is not a staff with the same teacher code in the the database then do not take the next pair of letters in the first name
            {
                break;
            }
        }

        if (staff != null) // if there is still a staff with the same teacher code and a unique teacher code cannot be created
        {
            for (int i = 0; i < lastName.Length - 1; i++) // loop through the last name until reaching the end
            {
                teacherCode = "X" + lastName.Substring(i, 2).ToUpper(); // set the teacher code to include X as the first letter then add a pair of letters from the last name until a unique teacher code is created

                staff = (from t1 in _context.Staff
                         where t1.TeacherCode == teacherCode
                         select t1).FirstOrDefault();

                if (staff == null) // the loop can break without the need for further cases because a unique teacher code would have already been already created realistically
                {
                    break;
                }
            }
        }

        Staff.TeacherCode = teacherCode; // sets the teacher code

        if (Staff.HireDate > DateTime.Now || Staff.HireDate < BeginningHireDate)
        {
            HireDateErrorMessage = "Invalid Hire Date. Hire Date must be after Avondale College was founded and cannot be in the future."; // displays error message
            return Page();
        }

        _context.Staff.Add(Staff);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}