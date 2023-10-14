using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hospital_management.DAL.Models
{
    public class Pharmacist
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 character", MinimumLength = 5)]
        public string? Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Firstname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Lastname { get; set; }

        [Required]
        public string? EmailId { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20, ErrorMessage = "Must be between 5 and 20 character", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string? ConfirmPassword { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public string? Specialization { get; set; }
        public DateTime HireDate { get; set; }
    }
}
