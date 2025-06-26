namespace ProjectMVC1.Models
{
    public class CrsResult
    {
        public int CrsResultID { get; set; }
        public double Degree { get; set; }
        public int CourseId { get; set; }
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }
        public Course Course { get; set; }
    }
}
