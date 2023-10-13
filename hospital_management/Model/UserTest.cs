using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_management.Model
{
    public class UserTest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        public Guid LabStaffId { get; set; }

        [ForeignKey("LabStaffId")]
        public LabStaff? LabStaff { get; set; }

        [Required]
        public decimal Total_Ptice { get; set; }
        public DateTime Test_Date { get; set; }
        public IEnumerable<TestDetail>? TestDetails { get; set; }
    }
}
