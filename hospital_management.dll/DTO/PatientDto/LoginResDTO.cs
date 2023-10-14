using hospital_management.DAL.Models;

namespace hospital_management.DAL.DTO.PatientDto
{
    public class LoginResDTO
    {
        public Patient Patient { get; set; }
        public string Token { get; set; }
    }
}
