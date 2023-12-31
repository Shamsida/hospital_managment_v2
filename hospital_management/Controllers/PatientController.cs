﻿//using hospital_management.service.Repository.Interface;
using hospital_management.BLL.Services.Interface;
using hospital_management.DAL.DTO.PatientDto;
using hospital_management.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stripe;
using System.Net;

namespace hospital_management.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IPatientService patientService;
        private Response res;

        public PatientController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IPatientService patientService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            this.patientService = patientService;
            res = new();
        }

        //[Authorize(Roles = "admin")]
        [HttpGet("api/patients/GetPatients")]
        public async Task<IActionResult> Get()
        {
            var itm = await patientService.Get();
            if (itm == null || !itm.Any())
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "There is no patients exist!";
                res.Success = false;
                return BadRequest(res);
            }
            res.StatusCode = HttpStatusCode.OK;
            res.Success = true;
            res.Result = itm;
            return Ok(res);
        }

        [HttpGet("api/patient/GetPatientById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var itm = await patientService.GetById(id);
            if (itm == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "This Id Doesnot exist!";
                res.Success = false;
                return BadRequest(res);
            }
            res.StatusCode = HttpStatusCode.OK;
            res.Success = true;
            res.Result = itm;
            return Ok(res);
        }

        [HttpGet("api/patient/GetPatientByUsername")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var itm = await patientService.GetByUsername(username);
            if (itm == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "This Username Doesnot exist!";
                res.Success = false;
                return BadRequest(res);
            }
            res.StatusCode = HttpStatusCode.OK;
            res.Success = true;
            res.Result = itm;
            return Ok(res);
        }

        [HttpPost("api/patient/Signup")]
        public async Task<IActionResult> SignUp([FromBody] CreatePatientDTO patientData)
        {
            var patient = await patientService.SignUP(patientData);
            if (patient == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "Invalid entry..Try again!";
                res.Success = false;
                return BadRequest(res);
            }
            IdentityUser user = new()
            {
                Email = patient.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = patient.Username,
                TwoFactorEnabled = true
            };
            if (await _roleManager.RoleExistsAsync(patient.Role))
            {
                var result = await _userManager.CreateAsync(user, patient.Password);
                if (!result.Succeeded)
                {
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Error = "User creation failed";
                    res.Success = false;
                    return BadRequest(res);
                }
                await _userManager.AddToRoleAsync(user, patient.Role);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Patient", new { token, email = patient.Email }, Request.Scheme);

                var emailContent = $"Please click the following link to confirm your email: {confirmationLink}";

                var message = new Message(new string[] { patient.Email! }, "Confirmation email link", emailContent);
                _emailService.SendEmail(message);

                res.StatusCode = HttpStatusCode.OK;
                res.StatusMessage = "Registered Successfully. Check your mail to verify emailId.";
                res.Success = true;
                res.Result = patient;
                return Ok(res);
            }
            else
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "User creation failed";
                res.Success = false;
                return BadRequest(res);
            }
        }

        [HttpGet("api/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var patient = await _userManager.FindByEmailAsync(email);
            if (patient != null)
            {
                var result = await _userManager.ConfirmEmailAsync(patient, token);
                if (result.Succeeded)
                {
                    res.StatusCode = HttpStatusCode.OK;
                    res.Success = true;
                    res.Result = "Email Verified Successfully";
                    return Ok(res);
                }
            }
            res.StatusCode = HttpStatusCode.BadRequest;
            res.Error = "This User Doesnot exist!";
            res.Success = false;
            return BadRequest(res);
        }

        [HttpPost("api/patient/Login")]
        public async Task<ActionResult> Login(LoginReqDTO loginReq)
        {
            var ptn = await patientService.Login(loginReq);
            if (ptn == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "Inavlid Credential";
                res.Success = false;
                return BadRequest(res);
            }
            res.StatusCode = HttpStatusCode.OK;
            res.StatusMessage = "Login Successfully";
            res.Success = true;
            res.Result = ptn;
            return Ok(res);
        }

        [HttpPut("api/patient/UpdatePatientData")]
        public async Task<IActionResult> Put(Guid Id, UpdatePatientDTO patientData)
        {
            var ptn = await patientService.Put(Id, patientData);
            if (ptn == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = "Inavlid Credential";
                res.Success = false;
                return BadRequest(res);
            }
            res.StatusCode = HttpStatusCode.OK;
            res.StatusMessage = "Data Updated Successfully";
            res.Success = true;
            res.Result = ptn;
            return Ok(res);
        }

        [HttpDelete("api/patient/DeletePatient")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var ptn = await patientService.Delete(Id);
                if (!ptn)
                {
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Error = "This Patient Doesnot exist!";
                    res.Success = false;
                    return BadRequest(res);
                }
                res.StatusCode = HttpStatusCode.OK;
                res.StatusMessage = "Delete Patient Successfully";
                res.Success = true;
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Error = ex.Message;
                res.Success = false;
                return BadRequest(res);
            }
        }

    }
}
