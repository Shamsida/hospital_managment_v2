using hospital_management.DAL.DTO.PatientDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital_management.BLL.Services.Interface
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDTO>> Get();
        Task<PatientDTO> GetById(Guid id);
        Task<PatientDTO> GetByUsername(string username);
        Task<PatientDataDTO> SignUP([FromForm] CreatePatientDTO patientData);
        Task<LoginResDTO> Login(LoginReqDTO loginReq);
        Task<bool> Delete(int Id);
    }
}
