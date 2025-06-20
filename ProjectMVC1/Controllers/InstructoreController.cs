using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public IActionResult Edit(int id) {
            var instructor = _context.Instructores.FirstOrDefault(i => i.InstructoreId == id);
            if (instructor == null)
                return NotFound();

            InstrctrWithDprtmntViewModel viewModel = new()
            {
                InstructoreId = instructor.InstructoreId,
                Name = instructor.Name,
                Salary = instructor.Salary,
                Address = instructor.Address,
                DepartmentId = instructor.DepartmentId,
                CourseId = instructor.CourseId,
                Image = instructor.Image,
                Departments = _context.Departments.ToList(),
                Courses = _context.Courses.ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SavaChanges(int id, InstrctrWithDprtmntViewModel updatedInstructore)
        {
            if (id != updatedInstructore.InstructoreId)
            {
                return BadRequest();
            }

            if(string.IsNullOrWhiteSpace(updatedInstructore.Name))
            {
                updatedInstructore.Departments = _context.Departments.ToList();
                updatedInstructore.Courses = _context.Courses.ToList();
                return View("Edit", updatedInstructore);
            }

            var orginal = _context.Instructores.FirstOrDefault(i => i.InstructoreId == id);
            if (orginal == null)
                return NotFound();

            orginal.Name = updatedInstructore.Name;
            orginal.Salary = updatedInstructore.Salary;
            orginal.Address = updatedInstructore.Address;
            orginal.Image = updatedInstructore.Image;
            orginal.DepartmentId = updatedInstructore.DepartmentId;
            orginal.CourseId = updatedInstructore.CourseId;

            _context.Attach(orginal);
            _context.Entry(orginal).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult New()
        {
            InstrctrWithDprtmntViewModel instructoreViewModel = new InstrctrWithDprtmntViewModel();
            instructoreViewModel.Departments = _context.Departments.ToList();
            instructoreViewModel.Courses = _context.Courses.ToList();
            return View(instructoreViewModel);
        }

        public IActionResult NewInstructor(InstrctrWithDprtmntViewModel NewInstructore) {
            if (NewInstructore.Name == null) {
                NewInstructore.Departments = _context.Departments.ToList();
                NewInstructore.Courses = _context.Courses.ToList();
                return View("New", NewInstructore);
            }

            Instructore instructor = new Instructore
            {
                Name = NewInstructore.Name,
                Address = NewInstructore.Address,
                Image = NewInstructore.Image,
                Salary = NewInstructore.Salary,
                DepartmentId = NewInstructore.DepartmentId,
                CourseId = NewInstructore.CourseId
            };

            _context.Instructores.Add(instructor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
