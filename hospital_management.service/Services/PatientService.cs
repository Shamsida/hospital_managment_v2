using AutoMapper;
using hospital_management.BLL.Services.Interface;
using hospital_management.DAL.Data;
using hospital_management.DAL.DTO.PatientDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital_management.BLL.Services
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly string secretkey;
        private readonly ILogger<PatientService> _logger;
        public PatientService(DataContext dataContext, IConfiguration configuration, IMapper mapper, IWebHostEnvironment env, ILogger<PatientService> logger)
        {
            _dataContext = dataContext;
            this.secretkey = configuration.GetValue<string>("Jwt:Key");
            _mapper = mapper;
            _environment = env;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientDTO>> Get()
        {
            try
            {
                var patient = await _dataContext.Patients
                            .ToListAsync();

                if (patient == null)
                {
                    throw new Exception("Invalid entry");
                }

                var patientDTO = patient.Select(u => new PatientDTO
                {
                    UserId = u.Id,
                    Username = u.Username,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Age = u.Age,
                    Gender = u.Gender,
                    Phone_Number = u.Phone_Number,
                    Address = u.Address
                }).ToList();

                return patientDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
        }

        public async Task<PatientDTO> GetById(Guid id)
        {
            try
            {
                var patient = await _dataContext.Patients
                            .Where(x => x.Role == "user")
                            .FirstOrDefaultAsync(u => u.Id == id);

                if (patient == null)
                {
                    throw new Exception("Invalid entry");
                }

                var patientDTO = new PatientDTO
                {
                    UserId = id,
                    Username = patient.Username,
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname,
                    Email = patient.Email,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Phone_Number = patient.Phone_Number,
                    Address = patient.Address
                };

                return patientDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
        }

        public async Task<PatientDTO> GetByUsername(string username)
        {
            try
            {
                var patient = await _dataContext.Patients
                            .Where(x => x.Role == "user")
                            .FirstOrDefaultAsync(u => u.Username == username);

                if (patient == null)
                {
                    throw new Exception("Invalid entry");
                }

                var patientDTO = new PatientDTO
                {
                    UserId = patient.Id,
                    Username = username,
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname,
                    Email = patient.Email,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Phone_Number = patient.Phone_Number,
                    Address = patient.Address
                };

                return patientDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
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
