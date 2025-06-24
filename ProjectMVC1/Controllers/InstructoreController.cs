using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMVC1.Models;
using X.PagedList.Extensions;

namespace ProjectMVC1.Controllers
{
    public class InstructoreController : Controller
    {
        private readonly MVCDbContext _context;

        public InstructoreController(MVCDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page, string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            var instructors = _context.Instructores
                .Include(i => i.Department)
                .Include(i => i.Course)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                instructors = instructors.Where(i =>
                    i.Name.Contains(search) || i.Address.Contains(search));
            }

            var pagedList = instructors.ToPagedList(pageNumber, pageSize);

            ViewBag.Search = search;
            return View(pagedList);
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
                OldImage = instructor.Image,
                Departments = _context.Departments.ToList(),
                Courses = _context.Courses.ToList(),
            };

            ViewBag.DeptList = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.CourseList = new SelectList(_context.Courses, "CourseId", "Name");

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SavaChanges(int id, InstrctrWithDprtmntViewModel updatedInstructore)
        {
            if (id != updatedInstructore.InstructoreId)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(updatedInstructore.Name))
            {
                updatedInstructore.Departments = _context.Departments.ToList();
                updatedInstructore.Courses = _context.Courses.ToList();
                return View("Edit", updatedInstructore);
            }

            if (updatedInstructore.ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(updatedInstructore.ImageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    updatedInstructore.ImageFile.CopyTo(stream);
                }

                updatedInstructore.Image = fileName;
            }
            else
            {
                updatedInstructore.Image = updatedInstructore.OldImage!;
            }

                var orginal = _context.Instructores.FirstOrDefault(i => i.InstructoreId == id);
            if (orginal == null)
                return NotFound();

            orginal.Name = updatedInstructore.Name;
            orginal.Salary = updatedInstructore.Salary;
            orginal.Address = updatedInstructore.Address;
            orginal.DepartmentId = updatedInstructore.DepartmentId;
            orginal.CourseId = updatedInstructore.CourseId;
            orginal.Image = updatedInstructore.Image;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult New()
        {
            InstrctrWithDprtmntViewModel instructoreViewModel = new InstrctrWithDprtmntViewModel();
            ViewBag.DeptList = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.CourseList = new SelectList(_context.Courses, "CourseId", "Name");
            return View(instructoreViewModel);
        }

        public IActionResult NewInstructor(InstrctrWithDprtmntViewModel NewInstructore) {
            if (string.IsNullOrWhiteSpace(NewInstructore.Name) || NewInstructore.ImageFile == null) {
                ViewBag.DeptList = new SelectList(_context.Departments, "DepartmentId", "Name");
                ViewBag.CourseList = new SelectList(_context.Courses, "CourseId", "Name");
                return View("New", NewInstructore);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(NewInstructore.ImageFile.FileName);
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                NewInstructore.ImageFile.CopyTo(stream);
            }

            NewInstructore.Image = fileName;

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

        public IActionResult Delete(int id)
        {
            var instructor = _context.Instructores
            .Include(i => i.Department)
            .Include(i => i.Course).FirstOrDefault(i => i.InstructoreId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", instructor.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Instructores.Remove(instructor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
