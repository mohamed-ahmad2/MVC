namespace ProjectMVC1.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public ICollection<Instructore> Instructores { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Trainee> Trainees { get; set; }
    }
}
