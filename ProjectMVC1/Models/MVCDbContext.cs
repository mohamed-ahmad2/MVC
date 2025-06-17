using Microsoft.EntityFrameworkCore;

namespace ProjectMVC1.Models
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext()
        {
        }

        public MVCDbContext(DbContextOptions<MVCDbContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructore> Instructores { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<CrsResult> CrsResults { get; set; }

    }
}
