using AutoMapper;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;

namespace courseProject.MappingProfile
{
    public class MappingProfileForStudentsInformation: Profile
    {


         public  MappingProfileForStudentsInformation (){
                CreateMap<Student, StudentsInformationDto>()
                    .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName));

            CreateMap<Student, ContactDto>()
                    .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName));


            CreateMap<RegistrationRequestDTO, Student>();
               // .ForMember(x=>x.Id , o=>o.MapFrom(y=>y.UserId));
            CreateMap<Student, RegistrationRequestDTO>();

            CreateMap<RegistrationRequestDTO, Admin>();
            CreateMap<Admin, RegistrationRequestDTO>();

            CreateMap<User, Student>()
                .ForMember(x => x.SId, o => o.MapFrom(y => y.UserId));

            CreateMap<User, Admin>()
                .ForMember(x => x.AId, o => o.MapFrom(y => y.UserId));

        }



        }
    }
