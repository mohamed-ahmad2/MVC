using ProjectMVC1.Repository.EntityRepository;

namespace ProjectMVC1.Repository
{
    public interface IUnitOfWork
    {
        IInstructoreRepository InstructoreRepository { get; }
        ICourseRepository CourseRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        void SaveChanges();
    }
}
