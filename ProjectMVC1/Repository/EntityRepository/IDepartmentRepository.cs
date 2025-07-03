using ProjectMVC1.Models;

namespace ProjectMVC1.Repository.EntityRepository
{
    public interface IDepartmentRepository
    {
        void Add(Department department);
        void Remove(Department department);
        Department? GetById(int id);
        IEnumerable<Department> GetAll();
    }
}
