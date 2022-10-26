namespace AvcolCoCurricularWebsite.Models;

public class Activity
{
    public int ActivityID { get; set; }

    [Display(Name = "Activity Name")]
    [StringLength(35, ErrorMessage = "Invalid Activity Name. Activity Name cannot be longer than 35 characters.")] // length of activity name cannot be longer than 35 characters because considering all the other activities are not longer than 35 characters, it is unlikely for an activity to be that long as it can be shortened
    [Required]
    public string ActivityName { get; set; }

    [Display(Name = "Room Number")]
    [RegularExpression(@"^[A-F][1-9][0-9]{0,1}", ErrorMessage = "Invalid Room Number. Room Number must contain either A, B, C, D, E, or F in capital letters at the beginning, and either 1 or 2 digits at the end.")]
    [StringLength(3, ErrorMessage = "Invalid Room Number. Room Number must be 3 characters long.")]
    [Required]
    public string RoomNumber { get; set; }

    [Display(Name = "Sign Up Form")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string SignUpForm { get; set; }

    [Display(Name = "Staff In Charge")]
    [Required]
    public int StaffID { get; set; }

    [Display(Name = "Staff In Charge")]
    public Staff Staff { get; set; }
}