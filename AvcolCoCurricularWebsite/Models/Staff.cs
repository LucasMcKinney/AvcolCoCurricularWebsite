namespace AvcolCoCurricularWebsite.Models;

public class Staff
{
    public int StaffID { get; set; }

    [Display(Name = "Last Name")]
    [StringLength(35, ErrorMessage = "Invalid Last Name. Last Name cannot be longer than 35 characters.")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string LastName { get; set; }

    [Display(Name = "First Name")]
    [StringLength(35, ErrorMessage = "Invalid First Name. First Name cannot be longer than 35 characters.")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string FirstName { get; set; }

    [Display(Name = "Teacher Code")]
    public string TeacherCode { get; set; }

    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public DateTime HireDate { get; set; }

    public string FullName
    {
        get
        {
            return LastName + " " + FirstName;
        }
    }

    public PersonalInformation PersonalInformation { get; set; }
}