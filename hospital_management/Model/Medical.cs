using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_management.Model
{
    public class Medical
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Diagnosis { get; set; }
        public string? Symptoms { get; set; }
        public double? Consultation_Fee { get; set; }
        public string? Status { get; set; }
        
        public DateTime VisitDate { get; set; }
    }
}
