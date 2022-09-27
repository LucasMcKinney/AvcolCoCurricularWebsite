using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Activities
{
    public class EditModel : PageModel
    {
        public static readonly string[] validBlock = { "A", "B", "C", "D", "E", "F" };

        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public EditModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; }
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

            //String[] validBlock = { "A", "B", "C", "D", "E", "F" };

            bool validRoom = true;
            var block = Activity.RoomNumber.ToUpper()[..1];

            if (validBlock.Contains(block))
            {
                char[] number = Activity.RoomNumber[1..].ToCharArray();
                int roomNumber = Int32.Parse(Activity.RoomNumber[1..]);

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
            else
            {
                _context.Activity.Add(Activity);
                await _context.SaveChangesAsync();
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
}