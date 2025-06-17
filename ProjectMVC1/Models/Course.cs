using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC1.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public double Degree { get; set; }
        public double MinDegree { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<CrsResult> CrsResults { get; set; }
        public virtual ICollection<Instructore> Instructores { get; set; }
    }
}
