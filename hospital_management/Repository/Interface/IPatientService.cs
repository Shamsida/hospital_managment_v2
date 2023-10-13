using hospital_management.DTO.PatientDto;
using hospital_management.Model;
using Microsoft.AspNetCore.Mvc;

namespace hospital_management.Repository.Interface
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> Get();
        Task<PatientDTO> GetById(Guid id);
        Task<PatientDTO> GetByUsername(string username);
        Task<PatientDataDTO> SignUP([FromForm] CreatePatientDTO patientData);
        Task<LoginResDTO> Login(LoginReqDTO loginReq);
        Task<bool> Delete(int Id);
    }
}
