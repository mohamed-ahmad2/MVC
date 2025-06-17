using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC1.Models;

namespace ProjectMVC1.Controllers
{
    public class InstructoreController : Controller
    {
        private readonly MVCDbContext _context;

        public InstructoreController(MVCDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var allInstructors = _context.Instructores
            .Include(i => i.Department)
            .Include(i => i.Course)
            .ToList();

            if (allInstructors == null)
            {
                return NotFound();
            }

            return View(allInstructors);
        }

        public IActionResult Details(int id) {
            var instructor = _context.Instructores
            .Include(i => i.Department)
            .Include(i => i.Course).FirstOrDefault(i => i.InstructoreId == id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }
    }
}
