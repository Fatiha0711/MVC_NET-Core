using System.ComponentModel.DataAnnotations;

namespace Work_03.Model.ViewModels
{
    public class StudentVM
    {
        public StudentVM()
        {
            this.SubjectList = new List<int>();
        }
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; } = default!;
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = default!;
        public IFormFile? ImageFile { get; set; }
        public string? Image { get; set; }
        public bool MorningShift { get; set; }

        public List<int> SubjectList { get; set; }
    }
}
