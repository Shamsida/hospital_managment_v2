using hospital_management.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace hospital_management.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medical> Medicals { get; set; }
        public DbSet<MedicalDetail> MedicalDetail { get; set; }
        public DbSet<Medicine> Medicines { get; set;}
    }
}
