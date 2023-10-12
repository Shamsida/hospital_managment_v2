using System.ComponentModel.DataAnnotations;

namespace hospital_management.Model
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection <Doctor>? Doctors { get; set; }
    }
}
