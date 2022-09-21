using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AvcolCoCurricularWebsite.Data;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Pages.Staff
{
    public class CreateModel : PageModel
    {
        private readonly AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext _context;

        public CreateModel(AvcolCoCurricularWebsite.Data.AvcolCoCurricularWebsiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Staff Staff { get; set; }

        private readonly DateTime BeginningHireDate = new DateTime(1945, 01, 01); // set BeginningHireDate to when avondale college started hiring staff

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var staffName = (from s in _context.Staff
                             where s.FirstName == Staff.FirstName && s.LastName == Staff.LastName
                             select s).FirstOrDefault(); // checks if there are any staff with the same first name and last name which already exists

            if (staffName != null)
            {
                ModelState.AddModelError("Custom", "Invalid Staff. Staff already exists."); // displays error message
                return Page();
            }

            var lastName = Staff.LastName;
            var teacherCode = " ";

            Models.Staff staff = null;

            for (int f = 0; f < Staff.FirstName.Length; f++) // loop through the first name until the end is reached
            {
                var firstLetters = Staff.FirstName.Substring(f, 1).ToUpper(); // first letter of the first name in uppercase

                for (int i = 0; i < lastName.Length - 1; i++) // check every pair of letters in the last name until a unique pair is found
                {
                    teacherCode = firstLetters + lastName.Substring(i, 2).ToUpper();

                    staff = (from s in _context.Staff
                             where s.TeacherCode == teacherCode
                             select s).FirstOrDefault();

                    if (staff == null) // if there is not a staff with the same name in the database then do not take the next pair of letters in the last name
                    {
                        break;
                    }
                }

                if (staff == null) // if there is not a staff with the same teacher code in the the database then do not take the next pair of letters in the first name
                {
                    break;
                }
            }

            if (staff != null) // if there is still a staff with the same teacher code and a unique teacher code cannot be created
            {
                for (int i = 0; i < lastName.Length - 1; i++) // loop through the last name until reaching the end
                {
                    teacherCode = "X" + lastName.Substring(i, 2).ToUpper(); // set the teacher code to include X as the first letter then add a pair of letters from the last name until a unique teacher code is created

                    staff = (from t1 in _context.Staff
                             where t1.TeacherCode == teacherCode
                             select t1).FirstOrDefault();

                    if (staff == null) // the loop can break without the need for further cases because a unique teacher code would have already been already created realistically
                    {
                        break;
                    }
                }
            }

            Staff.TeacherCode = teacherCode; // sets the teacher code

            if (Staff.HireDate > DateTime.Now || Staff.HireDate < BeginningHireDate)
            {
                ModelState.AddModelError("Custom", "Invalid Hire Date.");
                return Page();
            }

            _context.Staff.Add(Staff);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}