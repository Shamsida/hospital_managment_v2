using System.ComponentModel.DataAnnotations;

namespace hospital_management.DAL.Models
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection <Doctor>? Doctors { get; set; }
    }
}
