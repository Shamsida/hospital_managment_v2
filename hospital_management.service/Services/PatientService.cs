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
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
                            .Where(x => x.Role == "patient")
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
                if (await UserExists(patientData.Username))
                {
                    throw new Exception("Username is taken..Try another name");
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
                    Role = "patient"
                };

                _dataContext.Patients.Add(patient);
                await _dataContext.SaveChangesAsync();
                var ptntDto = _mapper.Map<PatientDataDTO>(patient);
                return ptntDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
        }

        private async Task<bool> UserExists(string username)
        {
            return await _dataContext.Patients.AnyAsync(x => x.Username == username.ToLower());
        }

        public async Task<LoginResDTO> Login(LoginReqDTO loginReq)
        {
            try
            {
                if (loginReq == null)
                {
                    throw new Exception("Invalid Enrty");
                };
                var ptn = await _dataContext.Patients
                          .SingleOrDefaultAsync(e => e.Username.ToLower() == loginReq.Username.ToLower() && e.Role == "patient");

                if (ptn == null || !BCrypt.Net.BCrypt.Verify(loginReq.Password, ptn.Password))
                {
                    throw new Exception("Invalid user name or password");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = Encoding.ASCII.GetBytes(secretkey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, ptn.Username),
                    new Claim(ClaimTypes.Role,ptn.Role)
                    }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new(new SymmetricSecurityKey(token), SecurityAlgorithms.HmacSha256)
                };
                var jwttoken = tokenHandler.CreateToken(tokenDescriptor);
                LoginResDTO loginResDTO = new LoginResDTO()
                {
                    Patient = ptn,
                    Token = tokenHandler.WriteToken(jwttoken)

                };
                return loginResDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
        }

        public async Task<Patient> Put(Guid Id, UpdatePatientDTO patientData)
        {
            try
            {
                var ptn = await _dataContext.Patients.FirstOrDefaultAsync(x => x.Id == Id);
                if (ptn == null)
                {
                    throw new Exception("Patient Not Found");
                }
                if (patientData == null)
                {
                    throw new Exception("Invalid entry");
                }

                if (!string.IsNullOrEmpty(patientData.Username))
                {
                    ptn.Username = patientData.Username;
                }

                if (!string.IsNullOrEmpty(patientData.Firstname))
                {
                    ptn.Firstname = patientData.Firstname;
                }

                if (!string.IsNullOrEmpty(patientData.Lastname))
                {
                    ptn.Lastname = patientData.Lastname;
                }

                if (!string.IsNullOrEmpty(patientData.Email))
                {
                    ptn.Email = patientData.Email;
                }

                if (patientData.Age.HasValue)
                {
                    ptn.Age = patientData.Age.Value;
                }

                if (!string.IsNullOrEmpty(patientData.Gender))
                {
                    ptn.Gender = patientData.Gender;
                }

                if (patientData.Phone_Number != 0)
                {
                    ptn.Phone_Number = patientData.Phone_Number;
                }

                if (!string.IsNullOrEmpty(patientData.Address))
                {
                    ptn.Address = patientData.Address;
                }

                await _dataContext.SaveChangesAsync();
                return ptn;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var ptn = await _dataContext.Patients.FirstOrDefaultAsync(x => x.Id == Id);
                if (ptn == null)
                {
                    throw new Exception("Not Found");
                }
                _dataContext.Patients.Remove(ptn);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
        }
    }
}
