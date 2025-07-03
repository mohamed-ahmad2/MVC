using ProjectMVC1.Models;

namespace ProjectMVC1.Repository.EntityRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MVCDbContext _context;

        public DepartmentRepository(MVCDbContext context)
        {
            _context = context;
        }

        public void Add(Department department)
        {
            if (department != null)
                _context.Departments.Add(department);
        }

        public void Remove(Department department)
        {
            _context.Departments.Remove(department);
        }

        public Department? GetById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }
    }
}
