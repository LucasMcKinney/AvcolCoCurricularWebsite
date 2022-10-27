namespace AvcolCoCurricularWebsite.Pages.Clubs;

public class CreateModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public CreateModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        // ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
        ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
        return Page();
    }

    [BindProperty]
    public Club Club { get; set; }
    //public Activity Activity { get; set; }
    public string StartTimeErrorMessage { get; set; }
    public string EndTimeErrorMessage { get; set; }
    public string RoomErrorMessage { get; set; }
    public string StaffErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Club.StartTime >= Club.EndTime)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            StartTimeErrorMessage = "Invalid Start Time. Start Time cannot be greater or equal to End Time."; // displays error message
            return Page();
        }

        if (Club.EndTime <= Club.StartTime)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            EndTimeErrorMessage = "Invalid End Time. End Time cannot be less than or equal to Start Time."; // displays error message
            return Page();
        }

        /*
        Club roomOccupied2 = (from c in _context.Club
                             where c.Activity.RoomNumber == Club.Activity.RoomNumber && c.Day == Club.Day // && c.StartTime >= Club.StartTime && c.EndTime <= Club.EndTime
                             select c).FirstOrDefault(); // checks if the room set for this activity is occupied between the same start and end times of the day specified on the activity
        */

        var activity = await _context.Activity.FindAsync(Club.ActivityID);

        if (activity == null)
        {
            // an error occured, please try again
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            StartTimeErrorMessage = "Invalid Start Time. Start Time cannot be greater or equal to End Time."; // displays error message
            return Page();
        }

        Club.Activity = activity;

        Club roomOccupied = _context.Club.Where(c => c.Activity.RoomNumber == Club.Activity.RoomNumber && c.Day == Club.Day).FirstOrDefault();

        if (roomOccupied != null)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            RoomErrorMessage = "The Room set for this activity is occupied at this time. Please change the Day, Start Time, or End Time."; // displays error message
            return Page();
        }

        /*
        Club staffOccupied = (from c in _context.Club
                              where c.Activity.StaffID == Club.Activity.StaffID && c.Day == Club.Day // && c.StartTime >= Club.StartTime && c.EndTime <= Club.EndTime
                              select c).FirstOrDefault(); // checks if the room set for this activity is occupied between the same start and end times of the day specified on the activity

        if (staffOccupied != null)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            StaffErrorMessage = "The Staff In Charge of this activity is occupied at this time. Please change the Day, Start Time, or End Time."; // displays error message
            return Page();
        }
        */

        _context.Club.Add(Club);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}