namespace AvcolCoCurricularWebsite.Pages.Activities;

public class CreateModel : PageModel
{
    private static readonly string[] _validBlock = { "A", "B", "C", "D", "E", "F" };

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
    public Activity Activity { get; set; }
    public string RoomNumberErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        bool validRoom = true;
        var block = Activity.RoomNumber.ToUpper()[..1];

        if (_validBlock.Contains(block))
        {
            char[] number = Activity.RoomNumber[1..].ToCharArray();
            int roomNumber = int.Parse(Activity.RoomNumber[1..]);

            foreach (char n in number)
            {
                if (!char.IsDigit(n))
                {
                    validRoom = false;
                }
                else if (block == "A")
                {
                    if (roomNumber < 1 || roomNumber > 46)
                    {
                        validRoom = false;
                    }
                    else
                    {
                        validRoom = true;
                        break;
                    }
                }
                else if (block == "B")
                {
                    if (roomNumber < 1 || roomNumber > 17)
                    {
                        validRoom = false;
                    }
                    else
                    {
                        validRoom = true;
                        break;
                    }
                }
                else if (block == "C")
                {
                    if (roomNumber < 1 || roomNumber > 29)
                    {
                        validRoom = false;
                    }
                    else
                    {
                        validRoom = true;
                        break;
                    }
                }
                else if (block == "D")
                {
                    if (roomNumber < 1 || roomNumber > 29)
                    {
                        validRoom = false;
                    }
                    else
                    {
                        validRoom = true;
                        break;
                    }
                }
                else if (block == "E")
                {
                    if (roomNumber < 1 || roomNumber > 12)
                    {
                        validRoom = false;
                    }
                    else
                    {
                        validRoom = true;
                        break;
                    }
                }
                else if (block == "F")
                {
                    if (roomNumber < 1 || roomNumber > 14)
                    {
                        validRoom = false;
                    }
                    else
                    {
                        validRoom = true;
                        break;
                    }
                }
            }
        }
        else
        {
            validRoom = false;
        }
        if (validRoom == false)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            RoomNumberErrorMessage = "This Room does not exist. Please type a valid Room Number, e.g. A37.";
            return Page();
        }

        _context.Activity.Add(Activity);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}