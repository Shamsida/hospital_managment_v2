using hospital_management.Model;

namespace hospital_management.DTO.PatientDto
{
    public class LoginResDTO
    {
        public Patient Patient { get; set; }
        public string Token { get; set; }
    }
}
