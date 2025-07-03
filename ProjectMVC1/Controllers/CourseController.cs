using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMVC1.Models;
using ProjectMVC1.Repository;

namespace ProjectMVC1.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var courses = _unitOfWork.CourseRepository.GetAllWithIncludes().ToList();
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
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewBag.DeptList = new SelectList(departments, "DepartmentId", "Name");
            return View(course);
        }

        [HttpPost]
        public IActionResult AddCourse(Course course) {
            if (ModelState.IsValid) {
                _unitOfWork.CourseRepository.Add(course);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewBag.DeptList = new SelectList(departments, "DepartmentId", "Name");
            return View("NewCourse", course);
        }
    }
}
