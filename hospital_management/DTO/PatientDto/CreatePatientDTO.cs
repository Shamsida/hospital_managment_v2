using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hospital_management.DTO.PatientDto
{
    public class CreatePatientDTO
    {
        [Required(ErrorMessage = "Username is Required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 character", MinimumLength = 5)]
        public string? Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Firstname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string? Email { get; set; }
        public double? Age { get; set; }
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20, ErrorMessage = "Must be between 5 and 20 character", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Phone]
        public double Phone_Number { get; set; } = 0;
        public string? Address { get; set; }
    }
}
