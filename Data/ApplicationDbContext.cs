
using Microsoft.EntityFrameworkCore;
using Patient_Portal.Models.Entity;
namespace Patient_Portal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Patient> Patients { get; set; }
    }
}
