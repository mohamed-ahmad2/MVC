namespace ProjectMVC1.Models
{
    public class InstrctrWithDprtmntViewModel
    {
        public int InstructoreId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public List<Department> Departments { get; set; }
        public List<Course> Courses { get; set; }
    }
}
