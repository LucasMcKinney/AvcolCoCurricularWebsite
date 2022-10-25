namespace AvcolCoCurricularWebsite.Pages.Activities;

public class CreateModel : PageModel
{
    private static readonly string[] validBlock = { "A", "B", "C", "D", "E", "F" };

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
    public string ActivityErrorMessage { get; set; }
    public string RoomNumberErrorMessage { get; set; }
    public string StaffErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var activityName = (from s in _context.Activity
                            where s.ActivityName == Activity.ActivityName
                            select s).FirstOrDefault(); // checks if there are any activities with the same name which already exists

        if (activityName != null)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            ActivityErrorMessage = "This Activity already has a record. Please edit the existing record."; // displays error message
            return Page();
        }
        if (!Activity.ActivityName.Any(char.IsLetter))
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            ActivityErrorMessage = "Invalid Activity Name. Activity Name must contain letters only."; // displays error message
            return Page();
        }

        bool validRoom = true;
        var block = Activity.RoomNumber[..1];

        if (validBlock.Contains(block))
        {
            char[] roomBlock = Activity.RoomNumber[1..].ToCharArray();
            int roomNumber = int.Parse(Activity.RoomNumber.AsSpan(1));

            foreach (char b in roomBlock)
            {
                if (block == "A")
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
            RoomNumberErrorMessage = "This Room Number does not exist. Please type a valid Room Number, e.g. A37."; // displays error message
            return Page();
        }

        Activity activity = (from a in _context.Activity
                             where a.StaffID == Activity.StaffID
                             select a).FirstOrDefault();

        if (activity != null)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            StaffErrorMessage = "This Staff is already in charge of an Activity. Please edit the existing record."; // displays error message
            return Page();
        }
        else
        {
            _context.Activity.Add(Activity);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}