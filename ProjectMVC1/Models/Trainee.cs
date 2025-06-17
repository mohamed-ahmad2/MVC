namespace ProjectMVC1.Models
{
    public class Trainee
    {
        public int TraineeId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<CrsResult> CrsResults { get; set; }
    }
}
