using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hospital_management.DAL.Models
{
    public class MedicalDetail
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid MedicalId { get; set; }
        public Guid MedicineId { get; set; }

        [ForeignKey("MedicineId ")]
        public Medicine? Medicine { get; set; }
        public int Quantity { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Total_Price { get; set; }
        public string? Description { get; set; }
    }
}
