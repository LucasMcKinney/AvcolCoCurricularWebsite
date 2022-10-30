namespace AvcolCoCurricularWebsite.Pages.ScholarshipTutorials;

public class EditModel : PageModel
{
    private readonly AvcolCoCurricularWebsiteContext _context;

    public EditModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ScholarshipTutorial ScholarshipTutorial { get; set; }
    public string StartTimeErrorMessage { get; set; }
    public string EndTimeErrorMessage { get; set; }
    public string ActivityErrorMessage { get; set; }
    public string RoomStaffOccupiedErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ScholarshipTutorial = await _context.ScholarshipTutorial
            .Include(s => s.Activity).FirstOrDefaultAsync(m => m.ScholarshipTutorialID == id);

        if (ScholarshipTutorial == null)
        {
            return NotFound();
        }
        ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (ScholarshipTutorial.StartTime >= ScholarshipTutorial.EndTime)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            StartTimeErrorMessage = "Invalid Start Time. Start Time cannot be greater or equal to End Time."; // displays error message
            return Page();
        }

        if (ScholarshipTutorial.EndTime <= ScholarshipTutorial.StartTime)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            EndTimeErrorMessage = "Invalid End Time. End Time cannot be less than or equal to Start Time."; // displays error message
            return Page();
        }

        var activity = await _context.Activity.FindAsync(ScholarshipTutorial.ActivityID);

        if (activity == null)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            ActivityErrorMessage = "An error occurred while processing this Activity. Please try again."; // displays error message
            return Page();
        }

        ScholarshipTutorial.Activity = activity;

        ScholarshipTutorial roomStaffOccupied = (from s in _context.ScholarshipTutorial
                                                 where s.Activity.RoomNumber == ScholarshipTutorial.Activity.RoomNumber && s.Activity.StaffID == ScholarshipTutorial.Activity.StaffID && s.Day == ScholarshipTutorial.Day && s.StartTime < ScholarshipTutorial.EndTime && s.EndTime > ScholarshipTutorial.StartTime
                                                 select s).FirstOrDefault(); // checks if the room and staff set for this activity is occupied between the same start and end times of the day specified on the activity

        if (roomStaffOccupied != null)
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "ActivityName");
            RoomStaffOccupiedErrorMessage = "The Room and Staff set for this activity is occupied between these times. Please change the Day, Start Time, or End Time."; // displays error message
            return Page();
        }

        _context.Attach(ScholarshipTutorial).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ScholarshipTutorialExists(ScholarshipTutorial.ScholarshipTutorialID))
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

    private bool ScholarshipTutorialExists(int id)
    {
        return _context.ScholarshipTutorial.Any(e => e.ScholarshipTutorialID == id);
    }
}