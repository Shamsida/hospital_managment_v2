﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hospital_management.DAL.DTO.PatientDto
{
    public class PatientDTO
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public double? Age { get; set; }
        public string? Gender { get; set; }
        public double Phone_Number { get; set; } = 0;
        public string? Address { get; set; }
    }
}
