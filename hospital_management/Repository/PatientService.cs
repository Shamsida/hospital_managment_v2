using AutoMapper;
using hospital_management.Data;
using hospital_management.DTO.PatientDto;
using hospital_management.Model;
using hospital_management.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital_management.Repository
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly string secretkey;
        public PatientService(DataContext dataContext, IConfiguration configuration, IMapper mapper, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            this.secretkey = configuration.GetValue<string>("Jwt:Key");
            _mapper = mapper;
            _environment = env;
        }

        public async Task<IEnumerable<Patient>> Get()
        {
            try
            {
                var patient = await _dataContext.Patients
                            .ToListAsync();
                
                return patient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<PatientDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PatientDTO> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResDTO> Login(LoginReqDTO loginReq)
        {
            throw new NotImplementedException();
        }

        public Task<PatientDataDTO> SignUP([FromForm] CreatePatientDTO patientData)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
