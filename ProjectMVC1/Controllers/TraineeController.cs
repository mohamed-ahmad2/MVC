using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC1.Models;

namespace ProjectMVC1.Controllers
{
    //29335

    public class TraineeController : Controller
    {

        readonly MVCDbContext _context;

        public TraineeController(MVCDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowResult(int id, int crsId)
        {
            var crsData = _context.CrsResults
                            .Include(i => i.Trainee)
                            .ThenInclude(t => t.Department)
                            .Include(i => i.Course)
                            .ThenInclude(c => c.Department)
                            .FirstOrDefault(i => i.TraineeId == id && i.CourseId == crsId);
            //var tranie = _context.Trainees.Include(i => i.Department).FirstOrDefault(i => i.TraineeId == id);
            //var course = _context.Courses.Include(i => i.Department).FirstOrDefault(i => i.CourseId == crsId);
            if (crsData == null /*|| tranie == null || course == null*/)
            {
                return NotFound();
            }

            //TraineeWithCourseViewModel traineeWithCourseViewModel = new()
            //{
            //    //Traine
            //    TraineeId = tranie.TraineeId,
            //    NameTrainee = tranie.Name,
            //    Image = tranie.Image,
            //    Address = tranie.Address,
            //    Grade = tranie.Grade,
            //    DepartmentIdTraine = tranie.DepartmentId,
            //    //Course
            //    CourseId = course.CourseId,
            //    NameCourse = course.Name,
            //    DegreeCourse = course.Degree,
            //    MinDegree = course.MinDegree,
            //    DepartmentIdCourse = course.DepartmentId,
            //    //CrsResult
            //    CrsResultID = crsData.CrsResultID,
            //    DegreeCrsResult = crsData.Degree,
            //    CourseIdCrsResult = crsData.CourseId,
            //    TraineeIdCrsResult = crsData.TraineeId
            //};

            return View(crsData);
        }
    }
}
