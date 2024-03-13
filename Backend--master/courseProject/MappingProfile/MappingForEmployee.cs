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
                .ForMember(x=>x.FName , o=>o.MapFrom(y=>y.user.userName))
                .ForMember(x => x.Id, o => o.MapFrom(y => y.SAId));

            CreateMap<Instructor, EmployeeDto>()
                .ForMember(x => x.FName, o => o.MapFrom(y => y.user.userName))
                .ForMember(x => x.Id, o => o.MapFrom(y => y.IId));

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
            CreateMap<EmployeeForCreate, SubAdmin>();
            CreateMap<EmployeeForCreate, Instructor>();



            CreateMap<EmployeeForCreate, RegistrationRequestDTO>()
               .ForMember(x => x.userName, o => o.MapFrom(y => y.FName))
            .ForMember(x => x.ConfirmPassword, o => o.MapFrom(y => y.password));


            CreateMap<SubAdmin, RegistrationRequestDTO>();
            CreateMap<Admin, RegistrationRequestDTO>();

            CreateMap<User, SubAdmin>()
              .ForMember(x => x.SAId, o => o.MapFrom(y => y.UserId));
            CreateMap<User, Instructor>()
              .ForMember(x => x.IId, o => o.MapFrom(y => y.UserId));


            CreateMap< ProfileDTO ,User >()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.FName))
                .ForMember(x=>x.email , o=>o.MapFrom(y=>y.email));

            CreateMap<ProfileDTO, Admin>();
            CreateMap<ProfileDTO, SubAdmin>();
            CreateMap<ProfileDTO, Instructor>();
            CreateMap<ProfileDTO, Student>();
            

        }
    }
}
