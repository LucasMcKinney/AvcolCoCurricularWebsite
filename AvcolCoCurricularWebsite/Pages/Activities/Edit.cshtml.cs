namespace AvcolCoCurricularWebsite.Pages.Activities;

public class EditModel : PageModel
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

    public EditModel(AvcolCoCurricularWebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Activity Activity { get; set; }
    public string ActivityErrorMessage { get; set; }
    public string RoomNumberErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Activity = await _context.Activity
            .Include(a => a.Staff).FirstOrDefaultAsync(m => m.ActivityID == id);

        if (Activity == null)
        {
            return NotFound();
        }
        ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!Activity.ActivityName.Any(char.IsLetter))
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "FullName");
            ActivityErrorMessage = "Invalid Activity Name. Activity Name must contain letters only."; // displays error message
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

        _context.Attach(Activity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActivityExists(Activity.ActivityID))
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

    private bool ActivityExists(int id)
    {
        return _context.Activity.Any(e => e.ActivityID == id);
    }
}