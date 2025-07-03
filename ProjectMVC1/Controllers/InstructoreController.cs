using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMVC1.Models;
using ProjectMVC1.Repository;
using X.PagedList.Extensions;

namespace ProjectMVC1.Controllers
{
    public class InstructoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? page, string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            IEnumerable<Instructore> instructors;

            if (!string.IsNullOrEmpty(search))
            {
                instructors = _unitOfWork.InstructoreRepository.GetBySearch(search);
            }
            else
            {
                instructors = _unitOfWork.InstructoreRepository.GetAll();
            }

            var pagedList = instructors.ToPagedList(pageNumber, pageSize);

            ViewBag.Search = search;
            return View(pagedList);
        }




        public IActionResult Details(int id) {
            var instructor = _unitOfWork.InstructoreRepository.GetByIdWithIncludes(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        public IActionResult Edit(int id) {
            var instructor = _unitOfWork.InstructoreRepository.GetById(id);
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
                Departments = _unitOfWork.DepartmentRepository.GetAll().ToList(),
                Courses = _unitOfWork.CourseRepository.GetAll().ToList(),
            };

            ViewBag.DeptList = new SelectList(viewModel.Departments, "DepartmentId", "Name");
            ViewBag.CourseList = new SelectList(viewModel.Courses, "CourseId", "Name");

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
                updatedInstructore.Departments = _unitOfWork.DepartmentRepository.GetAll().ToList();
                updatedInstructore.Courses = _unitOfWork.CourseRepository.GetAll().ToList();
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

            var orginal = _unitOfWork.InstructoreRepository.GetById(id);
            if (orginal == null)
                return NotFound();

            orginal.Name = updatedInstructore.Name;
            orginal.Salary = updatedInstructore.Salary;
            orginal.Address = updatedInstructore.Address;
            orginal.DepartmentId = updatedInstructore.DepartmentId;
            orginal.CourseId = updatedInstructore.CourseId;
            orginal.Image = updatedInstructore.Image;

            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult New()
        {
            InstrctrWithDprtmntViewModel instructoreViewModel = new InstrctrWithDprtmntViewModel
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().ToList(),
                Courses = _unitOfWork.CourseRepository.GetAll().ToList()
            };

            ViewBag.DeptList = new SelectList(instructoreViewModel.Departments, "DepartmentId", "Name");
            ViewBag.CourseList = new SelectList(instructoreViewModel.Courses, "CourseId", "Name");

            return View(instructoreViewModel);
        }

        public IActionResult NewInstructor(InstrctrWithDprtmntViewModel NewInstructore) {
            if (string.IsNullOrWhiteSpace(NewInstructore.Name) || NewInstructore.ImageFile == null) {
                NewInstructore.Departments = _unitOfWork.DepartmentRepository.GetAll().ToList();
                NewInstructore.Courses = _unitOfWork.CourseRepository.GetAll().ToList();

                ViewBag.DeptList = new SelectList(NewInstructore.Departments, "DepartmentId", "Name");
                ViewBag.CourseList = new SelectList(NewInstructore.Courses, "CourseId", "Name");
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

            _unitOfWork.InstructoreRepository.Add(instructor);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var instructor = _unitOfWork.InstructoreRepository.GetByIdWithIncludes(id);
            if (instructor == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", instructor.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _unitOfWork.InstructoreRepository.Remove(instructor);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
