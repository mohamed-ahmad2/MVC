using ProjectMVC1.Models;
using ProjectMVC1.Repository.EntityRepository;

namespace ProjectMVC1.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCDbContext _context;

        public ICourseRepository CourseRepository { get; }
        public IInstructoreRepository InstructoreRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }

        public UnitOfWork(MVCDbContext context)
        {
            _context = context;
            InstructoreRepository = new InstructoreRepository(_context);
            CourseRepository = new CourseRepository(_context);
            DepartmentRepository = new DepartmentRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
