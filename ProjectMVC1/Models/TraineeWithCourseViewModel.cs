using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC1.Models
{
    public class TraineeWithCourseViewModel
    {
        //Traine
        public int TraineeId { get; set; }
        public string NameTrainee { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }
        public int DepartmentIdTraine { get; set; }

        //course
        public int CourseId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        [Unique]
        public string NameCourse { get; set; }
        [Required]
        [Range(50, 100)]
        public double DegreeCourse { get; set; }
        [Required]
        [Remote(action: "CheckMinDegree", controller: "Course", AdditionalFields = "Degree", ErrorMessage = "MinDegree must be less than Degree.")]
        public double MinDegree { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please select a department")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department")]
        public int DepartmentIdCourse { get; set; }

        //CrsResult
        public int CrsResultID { get; set; }
        public double DegreeCrsResult { get; set; }
        public int CourseIdCrsResult { get; set; }
        public int TraineeIdCrsResult { get; set; }
    }
}
