using Microsoft.EntityFrameworkCore;

namespace EmployesApp
{
    class ApplicationContext : DbContext
    {
        public DbSet<Employe> Employes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        public ApplicationContext()
        {

            Database.EnsureCreated();       
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EmployesDB;Trusted_Connection=True;");

        }
    }
}
