using System.ComponentModel.DataAnnotations;

namespace hospital_management.Model
{
    public class Medicine
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Discount { get; set; }
        public int Total_Quantity { get; set; }
        public string? Diagnosis { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Uses { get; set; }
        public string? Side_Effects { get; set; }
        public string? Image { get; set; }
        public ICollection<MedicalDetail>? MedicalDetails { get; set; }
        public ICollection<MedicineDetail>? MedicineDetails { get; set; }
    }
}
