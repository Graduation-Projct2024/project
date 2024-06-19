using AutoMapper;
using courseProject.Common;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using Microsoft.OpenApi.Extensions;

namespace courseProject.MappingProfile
{
    public class MappingForEmployee : Profile
    {
      

        public MappingForEmployee()
        {
        



            CreateMap<SubAdmin, Instructor>().ReverseMap();
            CreateMap<User, EmployeeDto>()
          .ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.userName))
          .ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LName))
          .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
          .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.phoneNumber))
          .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.gender))
          .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address))
          .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
          .ForMember(dest => dest.dateOfAdded, opt => opt.MapFrom(src => src.dateOfAdded))
          .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.role));

            CreateMap<SubAdmin, EmployeeDto>()            
                .ForMember(x => x.Id, o => o.MapFrom(y => y.SubAdminId))
                .IncludeMembers(x => x.user);
                

            CreateMap<Instructor, EmployeeDto>()               
                .ForMember(x => x.Id, o => o.MapFrom(y => y.InstructorId))
                .IncludeMembers(x => x.user);

           


            CreateMap<EmployeeForCreate, User>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.FName));
            CreateMap<EmployeeDto, Instructor>();
            CreateMap<EmployeeDto, SubAdmin>();
            CreateMap<EmployeeForUpdateDTO, Instructor>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<EmployeeForUpdateDTO, SubAdmin>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<EmployeeForUpdateDTO, User>()
                .ForMember(x=>x.userName , o=>o.MapFrom(y => y.FName))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));

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
                .ForMember(x => x.userName, o => o.MapFrom(y => y.FName))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));


            CreateMap<ProfileDTO, Admin>()

               .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));

            CreateMap<ProfileDTO, SubAdmin>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<ProfileDTO, Instructor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<ProfileDTO, Student>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<Admin, ProfileDTO>();
            CreateMap<SubAdmin, ProfileDTO>();
            CreateMap<Instructor, ProfileDTO>();
            CreateMap<Student, ProfileDTO>();
            CreateMap<User, ProfileDTO>()
                .ForMember(x => x.FName, o => o.MapFrom(y => y.userName));
           

           

            CreateMap<User, UserInfoDTO>()
          .ForMember(x => x.DateOfBirth, o => o.MapFrom(y => y.DateOfBirth.HasValue ? y.DateOfBirth.Value.ToString("dd/MM/yyyy") : null));


            CreateMap<WorkingHourDTO, Instructor_Working_Hours>()
              .ForMember(x=>x.startTime , o=>o.MapFrom(y=>TimeSpan.Parse(y.startTime)))
                .ForMember(x => x.endTime, o => o.MapFrom(y => TimeSpan.Parse(y.endTime)));

            CreateMap<Instructor_Working_Hours, GetWorkingHourDTO>()
                .ForMember(x => x.startTime, o => o.MapFrom(y => y.startTime))
                .ForMember(x => x.endTime, o => o.MapFrom(y => y.endTime))
                .ForMember(x => x.day, o => o.MapFrom(y => y.day.GetDisplayName()));


            CreateMap<StudentConsultations, LecturesForRetriveDTO>()
                .ForMember(x => x.InstructoruserName, o => o.MapFrom(y => y.consultation.instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.consultation.instructor.user.LName))
                .ForMember(x => x.StudentuserName, o => o.MapFrom(y => y.Student.user.userName))
                .ForMember(x => x.StudentLName, o => o.MapFrom(y => y.Student.user.LName))
                .ForMember(x => x.date, o => o.MapFrom(y =>y.consultation.date.ToString("dd/MM/yyyy")));

            CreateMap<Instructor, EmployeeListDTO>()
                .ForMember(x=>x.id , o=>o.MapFrom(y=>y.InstructorId))
                .ForMember(x => x.name, o => o.MapFrom(y => y.user.userName+" "+ y.user.LName));


            CreateMap<Instructor_Working_Hours, Instructor_OfficeHoursDTO>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.instructor.user.userName))
                .ForMember(x => x.LName, o => o.MapFrom(y => y.instructor.user.LName));

            CreateMap<Instructor_Working_Hours, EmployeeListDTO>()
                .ForMember(x => x.name, o => o.MapFrom(y => y.instructor.user.userName + " " + y.instructor.user.LName))
                .ForMember(x=>x.id , o=>o.MapFrom(y=>y.InstructorId));
        }
    }
}
