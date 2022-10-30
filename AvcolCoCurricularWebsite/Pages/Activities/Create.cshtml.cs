namespace AvcolCoCurricularWebsite.Pages.Activities;

public class CreateModel : PageModel
{
    private static readonly Dictionary<char, int> _validRooms = new()
    {
        { 'A', 46 },
        { 'B', 17 },
        { 'C', 29 },
        { 'D', 29 },
        { 'E', 12 },
        { 'F', 14 }
    };

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

        bool validRoom = true;
        var block = Activity.RoomNumber[0];

        if (_validRooms.ContainsKey(block))
        {
            int roomNumber = int.Parse(Activity.RoomNumber.AsSpan(1));
            int maxRoomNumber = _validRooms[block];

            if (roomNumber < 1 || roomNumber > maxRoomNumber)
            {
                validRoom = false;
            }
        }
        else
        {
            validRoom = false;
        }

        if (!validRoom)
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            RoomNumberErrorMessage = "This Room Number does not exist. Please type a valid Room Number, e.g. D2 or A34."; // displays error message
            return Page();
        }
        
        _context.Activity.Add(Activity);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}