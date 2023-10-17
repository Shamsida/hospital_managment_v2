using hospital_management.DAL.DTO.PatientDto;
using hospital_management.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace hospital_management.BLL.Services.Interface
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDTO>> Get();
        Task<PatientDTO> GetById(Guid id);
        Task<PatientDTO> GetByUsername(string username);
        Task<PatientDataDTO> SignUP([FromForm] CreatePatientDTO patientData);
        Task<LoginResDTO> Login(LoginReqDTO loginReq);
        Task<Patient> Put(Guid Id, UpdatePatientDTO patientData);
        Task<bool> Delete(Guid Id);
    }
}
