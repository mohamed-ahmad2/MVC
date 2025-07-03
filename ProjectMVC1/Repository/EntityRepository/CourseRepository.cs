using Microsoft.EntityFrameworkCore;
using ProjectMVC1.Models;

namespace ProjectMVC1.Repository.EntityRepository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MVCDbContext _context;

        public CourseRepository(MVCDbContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            if (course != null)
                _context.Courses.Add(course);
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course? GetById(int id)
        {
            return _context.Courses.FirstOrDefault(c => c.CourseId == id);
        }

        public void Remove(Course course)
        {
            _context.Courses.Remove(course);
        }

        public IEnumerable<Course> GetAllWithIncludes()
        {
            return _context.Courses.Include(i => i.Department);
        }
    }
}
