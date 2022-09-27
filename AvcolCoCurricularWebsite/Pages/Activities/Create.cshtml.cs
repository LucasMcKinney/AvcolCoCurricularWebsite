using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Activities
{
    public class CreateModel : PageModel
    {
        public static readonly string[] validBlock = { "A", "B", "C", "D", "E", "F" };

        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public CreateModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
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

            _context.Activity.Add(Activity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}