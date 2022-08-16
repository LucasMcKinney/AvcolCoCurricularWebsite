using System;
using System.ComponentModel.DataAnnotations;

namespace AvcolCoCurricularWebsite.Models
{
    public class Music
    {
        public int MusicID { get; set; }

        [Display(Name = "Activity")]
        [Required]
        public int ActivityID { get; set; }

        [Display(Name = "Room Number")]
        [StringLength(3, ErrorMessage = "Invalid Room Number. Room Number must be 3 characters long.")]
        [Required]
        public string RoomNumber { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:hh-mm-ss}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "This field cannot be left empty.")]
        public DateTime EndTime { get; set; }

        public Activities Activities { get; set; }
    }
}