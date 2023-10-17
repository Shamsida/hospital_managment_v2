using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital_management.DAL.DTO.PatientDto
{
    public class UpdatePatientDTO
    {
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public double? Age { get; set; }
        public string? Gender { get; set; }
        public double Phone_Number { get; set; } = 0;
        public string? Address { get; set; }
    }
}
