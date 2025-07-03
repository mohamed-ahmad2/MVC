using ProjectMVC1.Models;

namespace ProjectMVC1.Repository.EntityRepository
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Remove(Course course);
        Course? GetById(int id);
        IEnumerable<Course> GetAll();
        IEnumerable<Course> GetAllWithIncludes();
    }
}
