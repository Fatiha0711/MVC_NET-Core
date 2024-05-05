using System.ComponentModel.DataAnnotations;

namespace Work_03.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; } = default!;
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = default!;
        public string Image { get; set; } = default!;
        public bool MorningShift { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
