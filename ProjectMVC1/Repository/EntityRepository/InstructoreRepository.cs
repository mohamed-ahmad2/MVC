using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectMVC1.Models;

namespace ProjectMVC1.Repository.EntityRepository
{
    public class InstructoreRepository : IInstructoreRepository
    {
        private readonly MVCDbContext _context;
        public InstructoreRepository(MVCDbContext context)
        {
            _context = context;
        }

        public void Add(Instructore instructore)
        {
            if(instructore != null)
                _context.Instructores.Add(instructore);
        }

        public IEnumerable<Instructore> GetAll()
        {
            return _context.Instructores.ToList();
        }

        public Instructore? GetById(int id)
        {
            return _context.Instructores.FirstOrDefault(i => i.InstructoreId == id);
        }

        public void Remove(Instructore instructore)
        {
            _context.Remove(instructore);
        }

        public Instructore? GetByIdWithIncludes(int id)
        {
            return _context.Instructores
                .Include(i => i.Department)
                .Include(i => i.Course)
                .FirstOrDefault(i => i.InstructoreId == id);
        }
        public IEnumerable<Instructore> GetBySearch(string search)
        {
            return _context.Instructores
                .Where(i => i.Name.Contains(search) || i.Address.Contains(search))
                .ToList();
        }
    }
}
