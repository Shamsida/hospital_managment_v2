using AutoMapper;
using hospital_management.DAL.DTO.PatientDto;
using hospital_management.DAL.Models;

namespace hospital_management.IL.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Patient, PatientDataDTO>().ReverseMap();
        }
    }
}
