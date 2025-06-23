using Microsoft.AspNetCore.Mvc;
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
    }
}
