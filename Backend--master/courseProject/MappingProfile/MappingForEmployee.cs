using AutoMapper;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using Microsoft.OpenApi.Extensions;

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
               

            CreateMap<ProfileDTO, Admin>();
            CreateMap<ProfileDTO, SubAdmin>();
            CreateMap<ProfileDTO, Instructor>();
            CreateMap<ProfileDTO, Student>();

            CreateMap<User, ProfileDTO>()
                .ForMember(x => x.FName, o => o.MapFrom(y => y.userName));
            //  .ForMember(x => x.LName, o => o.MapFrom(y => y.instructor.LName ));

            CreateMap<Instructor, UserInfoDTO>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
                .ForMember(x => x.role, o => o.MapFrom(y => y.user.role))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(y => y.DateOfBirth.HasValue ? y.DateOfBirth.Value.ToString("dd/MM/yyyy") : null));



            CreateMap<Admin, UserInfoDTO>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
                .ForMember(x => x.role, o => o.MapFrom(y => y.user.role))
                .ForMember(x => x.DateOfBirth, o => o.
                                    MapFrom(y => y.DateOfBirth.HasValue ? y.DateOfBirth.Value.ToString("dd/MM/yyyy") : null));


            CreateMap<SubAdmin, UserInfoDTO>()
           .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
           .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
           .ForMember(x => x.role, o => o.MapFrom(y => y.user.role))
           .ForMember(x => x.DateOfBirth, o => o.MapFrom(y => y.DateOfBirth.HasValue ? y.DateOfBirth.Value.ToString("dd/MM/yyyy") : null));


            CreateMap<Student, UserInfoDTO>()
           .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
           .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
           .ForMember(x => x.role, o => o.MapFrom(y => y.user.role))
           .ForMember(x => x.DateOfBirth, o => o.MapFrom(y => y.DateOfBirth.HasValue? y.DateOfBirth.Value.ToString("dd/MM/yyyy") :null)); 


            CreateMap<WorkingHourDTO, Instructor_Working_Hours>()
              .ForMember(x=>x.startTime , o=>o.MapFrom(y=>TimeSpan.Parse(y.startTime)))
                .ForMember(x => x.endTime, o => o.MapFrom(y => TimeSpan.Parse(y.endTime)));

            CreateMap<Instructor_Working_Hours, GetWorkingHourDTO>()
                .ForMember(x => x.startTime, o => o.MapFrom(y => y.startTime))
                .ForMember(x => x.endTime, o => o.MapFrom(y => y.endTime))
                .ForMember(x => x.day, o => o.MapFrom(y => y.day.GetDisplayName()));


            CreateMap<Consultation, LecturesForRetriveDTO>()
                .ForMember(x => x.InstructoruserName, o => o.MapFrom(y => y.instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.instructor.LName))
                .ForMember(x => x.StudentuserName, o => o.MapFrom(y => y.student.user.userName))
                .ForMember(x => x.StudentLName, o => o.MapFrom(y => y.student.LName))
                .ForMember(x => x.date, o => o.MapFrom(y =>y.date.ToString("dd/MM/yyyy")));

            CreateMap<Instructor, EmployeeListDTO>()
                .ForMember(x=>x.id , o=>o.MapFrom(y=>y.InstructorId))
                .ForMember(x => x.name, o => o.MapFrom(y => y.user.userName+" "+ y.LName));
        }
    }
}
