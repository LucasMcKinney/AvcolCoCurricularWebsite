﻿namespace AvcolCoCurricularWebsite.Pages.Sports;

public class CreateModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public CreateModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
    ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
        return Page();
    }

    [BindProperty]
    public Sport Sport { get; set; }
    public string StartTimeErrorMessage { get; set; }
    public string EndTimeErrorMessage { get; set; }
    public string ActivityErrorMessage { get; set; }
    public string RoomStaffOccupiedErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Sport.StartTime >= Sport.EndTime)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            StartTimeErrorMessage = "Invalid Start Time. Start Time cannot be greater or equal to End Time."; // displays error message
            return Page();
        }

        if (Sport.EndTime <= Sport.StartTime)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            EndTimeErrorMessage = "Invalid End Time. End Time cannot be less than or equal to Start Time."; // displays error message
            return Page();
        }

        var activity = await _context.Activity.FindAsync(Sport.ActivityID);

        if (activity == null)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            ActivityErrorMessage = "An error occurred while processing this Activity. Please try again."; // displays error message
            return Page();
        }

        Sport.Activity = activity;

        Sport roomStaffOccupied = (from s in _context.Sport
                                   where s.Activity.RoomNumber == Sport.Activity.RoomNumber && s.Activity.StaffID == Sport.Activity.StaffID && s.Day == Sport.Day && s.StartTime < Sport.EndTime && s.EndTime > Sport.StartTime
                                   select s).FirstOrDefault(); // checks if the room and staff set for this activity is occupied between the same start and end times of the day specified on the activity

        if (roomStaffOccupied != null)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            RoomStaffOccupiedErrorMessage = "The Room and Staff set for this activity is occupied between these times. Please change the Day, Start Time, or End Time."; // displays error message
            return Page();
        }

        _context.Sport.Add(Sport);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}