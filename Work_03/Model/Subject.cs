namespace Work_03.Model
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = default!;
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
