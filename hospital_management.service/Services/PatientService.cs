using AutoMapper;
using hospital_management.BLL.Services.Interface;
using hospital_management.DAL.Data;
using hospital_management.DAL.DTO.PatientDto;
using hospital_management.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace hospital_management.BLL.Services
{
    public class PatientService : IPatientService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly string secretkey;
        private readonly ILogger<PatientService> _logger;

        public PatientService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            DataContext dataContext, IConfiguration configuration, IMapper mapper, IWebHostEnvironment env, IEmailService emailService,
            ILogger<PatientService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
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
                            .Where(x => x.Role == "patient")
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
                            .Where(x => x.Role == "")
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
        public async Task<PatientDataDTO> SignUP([FromBody] CreatePatientDTO patientData)
        {
            try
            {
                if (patientData == null)
                {
                    throw new Exception("Invalid entry");
                }
                var userExist = await _userManager.FindByEmailAsync(patientData.Email);
                if (userExist != null)
                {
                    throw new Exception("User already exists!");
                }

                var patient = new Patient
                {
                    Username = patientData.Username,
                    Firstname = patientData.Firstname,
                    Lastname = patientData.Lastname,
                    Email = patientData.Email,
                    Age = patientData.Age,
                    Gender = patientData.Gender,
                    Password = BCrypt.Net.BCrypt.HashPassword(patientData.Password, 10),
                    Phone_Number = patientData.Phone_Number,
                    Address = patientData.Address,
                    Role = "patient",
                    IsMailConfiormed = false
                };

                _dataContext.Patients.Add(patient);
                await _dataContext.SaveChangesAsync();
                var ptntDto = _mapper.Map<PatientDataDTO>(patient);
                return ptntDto;
                //return ptntDto;
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

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
