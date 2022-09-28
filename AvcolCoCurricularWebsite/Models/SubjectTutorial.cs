namespace AvcolCoCurricularWebsite.Models;

public class SubjectTutorial
{
    public int SubjectTutorialID { get; set; }

    [Display(Name = "Activity")]
    [Required]
    public int ActivityID { get; set; }

    public Activity Activity { get; set; }

    [Display(Name = "Day")]
    [Required]
    public DayOfWeek Day { get; set; }

    [Display(Name = "Start Time")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public DateTime StartTime { get; set; }

    [Display(Name = "End Time")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "This field cannot be left empty.")]
    public DateTime EndTime { get; set; }
}