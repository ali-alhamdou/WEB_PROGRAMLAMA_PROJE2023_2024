using Microsoft.EntityFrameworkCore;

namespace Api.DataAccessLayer
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS;database=APIHospitalAutomation; integrated security=True; TrustServerCertificate=True");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
