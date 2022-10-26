namespace AvcolCoCurricularWebsite.Models;

public class PersonalInformation
{
    [Display(Name = "Staff")]
    [Key]
    public int StaffID { get; set; }

    public Staff Staff { get; set; }

    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "Address")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string Address { get; set; }

    [Display(Name = "Phone Number")]
    [Phone]
    [StringLength(10, ErrorMessage = "Invalid Phone Number. Please enter a valid Phone Number under 10 digits long.")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Emergency Contact Phone Number")]
    [Phone]
    [StringLength(10, ErrorMessage = "Invalid Phone Number. Please enter a valid Phone Number under 10 digits long.")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string ECPhoneNumber { get; set; }
    
    [Display(Name = "Emergency Contact Relationship")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Emergency Contact Relationship. Emergency Contact Relationship must contain letters only.")]
    [StringLength(56, ErrorMessage = "Invalid Relationship. Please enter a valid Relationship under 56 characters long (e.g Brother, Friend, etc).")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string ECRelationship { get; set; }
    
    [Display(Name = "Citizen Status")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string CitizenStatus { get; set; }
    
    [Display(Name = "Ethnicity")]
    [StringLength(56, ErrorMessage = "Invalid Ethnic Country. The longest name of a country in the world is 56 characters long.")]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public string Ethnicity { get; set; }

    [Display(Name = "Email Address")]
    [EmailAddress]
    public string EmailAddress { get; set; }
}