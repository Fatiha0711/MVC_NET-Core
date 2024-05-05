using System.ComponentModel.DataAnnotations.Schema;

namespace Work_03.Model
{
    public class StudentSubject
    {
        public int StudentSubjectId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        //nev
        public virtual Subject? Subjects { get; set; }
        public virtual Student? Students { get; set; }
    }
}
