using AutoMapper;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;

namespace courseProject.MappingProfile
{
    public class MappingForEmployee : Profile
    {
        public MappingForEmployee()
        {

            CreateMap<SubAdmin , EmployeeDto>()
                .ForMember(x=>x.FName , o=>o.MapFrom(y=>y.user.userName));

            CreateMap<Instructor, EmployeeDto>()
                .ForMember(x => x.FName, o => o.MapFrom(y => y.user.userName));

            CreateMap<SubAdmin, ContactDto>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName));

            CreateMap<Instructor, ContactDto>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName));

            CreateMap<EmployeeForCreate, SubAdmin>();
            CreateMap<EmployeeForCreate, User>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.FName));
            CreateMap<EmployeeDto, Instructor>();
            CreateMap<EmployeeDto, SubAdmin>();
            CreateMap<updateEmployeeDTO, SubAdmin>();




            CreateMap<EmployeeForCreate, RegistrationRequestDTO>()
               .ForMember(x => x.userName, o => o.MapFrom(y => y.FName))
            .ForMember(x => x.ConfirmPassword, o => o.MapFrom(y => y.password));


            CreateMap<SubAdmin, RegistrationRequestDTO>();

        }
    }
}
