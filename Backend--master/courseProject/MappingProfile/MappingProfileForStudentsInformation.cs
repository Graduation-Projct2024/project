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
            CreateMap<Student, RegistrationRequestDTO>();

            CreateMap<RegistrationRequestDTO, Admin>();
            CreateMap<Admin, RegistrationRequestDTO>();
        }



        }
    }
