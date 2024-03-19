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
                .ForMember(x => x.Id, o => o.MapFrom(y => y.SubAdminId))
                .ForMember(x=>x.email , o=>o.MapFrom(y=>y.user.email));

            CreateMap<Instructor, EmployeeDto>()
                .ForMember(x => x.FName, o => o.MapFrom(y => y.user.userName))
                .ForMember(x => x.Id, o => o.MapFrom(y => y.InstructorId))
                .ForMember(x => x.email, o => o.MapFrom(y => y.user.email));

            CreateMap<SubAdmin, ContactDto>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
                .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.ImageUrl}"));

            CreateMap<Instructor, ContactDto>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
                .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.ImageUrl}"));

            CreateMap<EmployeeForCreate, SubAdmin>();
            CreateMap<EmployeeForCreate, User>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.FName));
            CreateMap<EmployeeDto, Instructor>();
            CreateMap<EmployeeDto, SubAdmin>();
            CreateMap<EmployeeForUpdateDTO, Instructor>();
            CreateMap<EmployeeForUpdateDTO, SubAdmin>();
            CreateMap<EmployeeForUpdateDTO, User>()
                .ForMember(x=>x.userName , o=>o.MapFrom(y => y.FName));
            CreateMap<updateEmployeeDTO, SubAdmin>()
                .ForMember(x=>x.SubAdminId , o=>o.MapFrom(y=>y.Id));
            CreateMap<EmployeeForCreate, SubAdmin>();
            CreateMap<EmployeeForCreate, Instructor>();



            CreateMap<EmployeeForCreate, RegistrationRequestDTO>()
               .ForMember(x => x.userName, o => o.MapFrom(y => y.FName))
            .ForMember(x => x.ConfirmPassword, o => o.MapFrom(y => y.password));


            CreateMap<SubAdmin, RegistrationRequestDTO>();
            CreateMap<Admin, RegistrationRequestDTO>();

            CreateMap<User, SubAdmin>()
              .ForMember(x => x.SubAdminId, o => o.MapFrom(y => y.UserId));
            CreateMap<User, Instructor>()
              .ForMember(x => x.InstructorId, o => o.MapFrom(y => y.UserId));


            CreateMap<ProfileDTO, User>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.FName));
               // .ForMember(x=>x.email , o=>o.MapFrom(y=>y.email));

            CreateMap<ProfileDTO, Admin>();
            CreateMap<ProfileDTO, SubAdmin>();
            CreateMap<ProfileDTO, Instructor>();
            CreateMap<ProfileDTO, Student>();

            CreateMap<User, ProfileDTO>()
                .ForMember(x=>x.FName , o=>o.MapFrom(y=>y.userName));
        }
    }
}
