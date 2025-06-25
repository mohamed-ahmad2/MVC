using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC1.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        [Unique]
        public string Name { get; set; }
        [Required]
        [Range(50, 100)]
        public double Degree { get; set; }
        [Required]
        [Remote(action: "CheckMinDegree", controller: "Course", AdditionalFields = "Degree", ErrorMessage = "MinDegree must be less than Degree.")]
        public double MinDegree { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please select a department")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public virtual ICollection<CrsResult>? CrsResults { get; set; }
        public virtual ICollection<Instructore>? Instructores { get; set; }
    }
}
