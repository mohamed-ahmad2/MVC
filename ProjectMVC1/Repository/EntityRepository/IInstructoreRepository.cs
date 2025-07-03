using ProjectMVC1.Models;

namespace ProjectMVC1.Repository.EntityRepository
{
    public interface IInstructoreRepository
    {
         void Add(Instructore instructore);
         void Remove(Instructore instructore);
         
         Instructore? GetById(int id);
         IEnumerable<Instructore> GetAll();
        Instructore? GetByIdWithIncludes(int id);
        IEnumerable<Instructore> GetBySearch(string search);
    }
}
