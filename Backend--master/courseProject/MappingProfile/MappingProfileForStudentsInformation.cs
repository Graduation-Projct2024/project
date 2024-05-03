using AutoMapper;
using courseProject.core.Models;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;

namespace courseProject.MappingProfile
{
    public class MappingProfileForStudentsInformation: Profile
    {


         public  MappingProfileForStudentsInformation (){
                CreateMap<Student, StudentsInformationDto>()
                    .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                    .ForMember(x => x.email, o => o.MapFrom(y => y.user.email));

            CreateMap<Student, ContactDto>()
                    .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                    .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
                    .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.ImageUrl}"));


            CreateMap<RegistrationRequestDTO, Student>();
               // .ForMember(x=>x.Id , o=>o.MapFrom(y=>y.UserId));
            CreateMap<Student, RegistrationRequestDTO>();

            CreateMap<RegistrationRequestDTO, Admin>();
            CreateMap<Admin, RegistrationRequestDTO>();

            CreateMap<User, Student>()
                .ForMember(x => x.StudentId, o => o.MapFrom(y => y.UserId));

            CreateMap<User, Admin>()
                .ForMember(x => x.AdminId, o => o.MapFrom(y => y.UserId));


            CreateMap<Student_Task_Submissions, StudentSubmissionDTO>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.Student.user.userName))
                .ForMember(x => x.LName, o => o.MapFrom(y => y.Student.LName))
                .ForMember(x => x.email, o => o.MapFrom(y => y.Student.user.email))
                .ForMember(x => x.pdfUrl, o => {
                    o.PreCondition(src => src.pdfUrl != null);
                    o.MapFrom(y => $"http://localhost:5134/{y.pdfUrl}");
                    });


            CreateMap<BookALectureDTO, Consultation>();
            CreateMap<FeedbackDTO, Feedback>();
            CreateMap<Feedback, FeedbackForRetriveDTO>()
                .ForMember(x => x.name, o => o.MapFrom(y => y.User.userName + " " + y.User.student.LName));

            CreateMap<Feedback, AllFeedbackForRetriveDTO>()
                .ForMember(x => x.name, o => o.MapFrom(y => y.User.userName + " " + y.User.student.LName));
        }



        }
    }
