using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMVC1.Models;

namespace ProjectMVC1.Controllers
{
    public class CourseController : Controller
    {
        readonly MVCDbContext _context;

        public CourseController(MVCDbContext mVCDbContext)
        {
            _context = mVCDbContext;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses
                .Include(i => i.Department)
                .ToList();
            return View(courses);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckMinDegree(double minDegree, double degree)
        {
            if (minDegree >= degree)
            {
                return Json("MinDegree must be less than Degree.");
            }

            return Json(true);
        }

        public IActionResult NewCourse()
        {
            Course course = new Course();
            ViewBag.DeptList = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View(course);
        }

        [HttpPost]
        public IActionResult AddCourse(Course course) {
            if (ModelState.IsValid) {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptList = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View("NewCourse", course);
        }
    }
}
