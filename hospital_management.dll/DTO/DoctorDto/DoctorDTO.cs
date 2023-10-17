using hospital_management.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital_management.DAL.DTO.DoctorDto
{
    public class DoctorDTO
    {
        public Guid DepartmentId { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public double Phone_Number { get; set; }
        public string? Specialist_In { get; set; }
    }
}
