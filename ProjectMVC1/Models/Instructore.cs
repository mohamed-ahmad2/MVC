namespace ProjectMVC1.Models
{
    public class Instructore
    {
        public int InstructoreId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public Department Department { get; set; }
        public Course Course { get; set; }
    }
}
