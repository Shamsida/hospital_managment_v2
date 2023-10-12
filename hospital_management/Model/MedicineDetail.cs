using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_management.Model
{
    public class MedicineDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MedicineId { get; set; }
        public double Searial_Number { get; set; }
        public DateTime Stock_date { get; set; }
        public int Quantity { get; set; }
        public DateTime Expiry_Date { get; set; }
    }
}
