using AutoMapper;
using courseProject.core.Models;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models.DTO.UsersDTO;

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

            CreateMap<Consultation, StudentConsultations>()
                .ForMember(x=>x.consultationId , o=>o.MapFrom(y=>y.Id));

            CreateMap<StudentConsultations, PublicLectureForRetriveDTO>()
                .ForMember(x => x.name, o => o.MapFrom(y => y.consultation.name))
                //.ForMember(dest => dest.StudentLName, opt => opt.MapFrom(src => new List<string> { src.Student.LName }))
               // .ForMember(dest => dest.StudentuserName, opt => opt.MapFrom(src => new List<string> { src.Student.user.userName }))
                .ForMember(x => x.InstructoruserName, o => o.MapFrom(y => y.consultation.instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.consultation.instructor.LName))
                .ForMember(x=>x.endTime , o=>o.MapFrom(y=>y.consultation.endTime))
                .ForMember(x => x.startTime, o => o.MapFrom(y => y.consultation.startTime))
                .ForMember(x => x.Duration, o => o.MapFrom(y => y.consultation.Duration))
                .ForMember(x => x.description, o => o.MapFrom(y => y.consultation.description))
                .ForMember(x => x.type, o => o.MapFrom(y => y.consultation.type))
                .ForMember(x => x.InstructorId, o => o.MapFrom(y => y.consultation.InstructorId))               
                .ForMember(x => x.date, o => o.MapFrom(y => y.consultation.date.ToString("dd/MM/yyyy")));


            CreateMap<StudentConsultations, UserNameDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(y => y.Student.user.userName + " " + y.Student.LName));

            CreateMap<Consultation, LecturesForRetriveDTO>()
                .ForMember(x => x.InstructoruserName, o => o.MapFrom(y => y.instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.instructor.LName))
                .ForMember(x => x.StudentuserName, o => o.MapFrom(y => y.student.user.userName))
                .ForMember(x => x.StudentLName, o => o.MapFrom(y => y.student.LName));


            CreateMap<StudentCourse, ViewTheRequestOfJoindCourseDTO>()
                .ForMember(x => x.StudentName, o => o.MapFrom(y => y.Student.user.userName + " " + y.Student.LName))
                .ForMember(x => x.CourseName, o => o.MapFrom(y => y.Course.name))
                .ForMember(x => x.EnrollDate, o => o.MapFrom(y => y.EnrollDate.Date.ToString("dd/MM/yyyy")));
        }



        }
    }
