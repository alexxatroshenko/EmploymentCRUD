using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
