using hospital_management.DAL.DTO.DoctorDto;
using hospital_management.DAL.DTO.PatientDto;
using hospital_management.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital_management.BLL.Services.Interface
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> Get();
        Task<DoctorDTO> GetById(Guid id);
        Task<DoctorDTO> GetByName(string username);
        Task<DoctorDTO> GetByDepartmentId(Guid id);
        Task<DoctorDataDTO> SignUP([FromForm] CreateDoctorDTO patientData);
        Task<LoginResDTO> Login(LoginReqDTO loginReq);
        Task<Doctor> Put(Guid Id, UpdateDoctorDTO patientData);
        Task<bool> Delete(Guid Id);
    }
}
