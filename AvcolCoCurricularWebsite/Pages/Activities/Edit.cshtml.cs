﻿namespace AvcolCoCurricularWebsite.Pages.Activities;

public class EditModel : PageModel
{
    private static readonly string[] validBlock = { "A", "B", "C", "D", "E", "F" };

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
            ActivityErrorMessage = "Invalid Activity Name. Activity Name must contain letters only."; // displays error message
            return Page();
        }

        bool validRoom = true;
        var block = Activity.RoomNumber[..1];

        if (validBlock.Contains(block))
        {
            char[] roomBlock = Activity.RoomNumber[1..].ToUpper().ToCharArray();
            int roomNumber = int.Parse(Activity.RoomNumber[1..]);

            foreach (char b in roomBlock)
            {
                if (char.IsDigit(b)) // if any character in roomBlock is a digit then validRoom is false
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
                else if(block == "B")
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
            RoomNumberErrorMessage = "This Room does not exist. Please type a valid Room, e.g. A37."; // displays error message
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