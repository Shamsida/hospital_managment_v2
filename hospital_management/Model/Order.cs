using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_management.Model
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        public Guid PharmacistId { get; set; }

        [ForeignKey("PharmacistId")]
        public Pharmacist? Pharmacist { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public int Total_Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
