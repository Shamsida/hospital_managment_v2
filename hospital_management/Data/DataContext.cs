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
        public DbSet<MedicalDetail> MedicalDetails { get; set; }
        public DbSet<Medicine> Medicines { get; set;}
        public DbSet<MedicineDetail> MedicineDetails { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LabStaff> LabStaffs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<TestDetail> TestDetails { get; set; }

    }
}
