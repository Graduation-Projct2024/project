using AutoMapper;
using courseProject.core.Models;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models.DTO.UsersDTO;

namespace courseProject.MappingProfile
{
    public class MappingForStudents: Profile
    {
      

        public  MappingForStudents ()
        {
                CreateMap<Student, StudentsInformationDto>()
                    .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                    .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
                    .ForMember(x => x.LName, o => o.MapFrom(y => y.user.LName))
                    .ForMember(x => x.ImageUrl, o => o.MapFrom(y => y.user.ImageUrl))
                    .ForMember(x => x.phoneNumber, o => o.MapFrom(y => y.user.phoneNumber))
                    .ForMember(x => x.gender, o => o.MapFrom(y => y.user.gender))
                    .ForMember(x => x.address, o => o.MapFrom(y => y.user.address));

            CreateMap<Student, ContactDto>()
                    .ForMember(x => x.userName, o => o.MapFrom(y => y.user.userName))
                    .ForMember(x => x.email, o => o.MapFrom(y => y.user.email))
                    .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"https://localhost:7116/{y.user.ImageUrl}"));


            CreateMap<RegistrationRequestDTO, Student>();          
            CreateMap<Student, RegistrationRequestDTO>();

            CreateMap<RegistrationRequestDTO, Admin>();
            CreateMap<Admin, RegistrationRequestDTO>();

            CreateMap<User, Student>()
                .ForMember(x => x.StudentId, o => o.MapFrom(y => y.UserId));

            CreateMap<User, Admin>()
                .ForMember(x => x.AdminId, o => o.MapFrom(y => y.UserId));


            CreateMap<Student_Task_Submissions, StudentSubmissionDTO>()
                .ForMember(x => x.userName, o => o.MapFrom(y => y.Student.user.userName))
                .ForMember(x => x.LName, o => o.MapFrom(y => y.Student.user.LName))
                .ForMember(x => x.email, o => o.MapFrom(y => y.Student.user.email));
               


            CreateMap<BookALectureDTO, Consultation>();
           

            CreateMap<Consultation, StudentConsultations>()
                .ForMember(x=>x.consultationId , o=>o.MapFrom(y=>y.Id));

            CreateMap<StudentConsultations, PublicLectureForRetriveDTO>()
                .ForMember(x => x.name, o => o.MapFrom(y => y.consultation.name))
                .ForMember(x => x.InstructoruserName, o => o.MapFrom(y => y.consultation.instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.consultation.instructor.user.LName))
                .ForMember(x=>x.endTime , o=>o.MapFrom(y=>y.consultation.endTime))
                .ForMember(x => x.startTime, o => o.MapFrom(y => y.consultation.startTime))
                .ForMember(x => x.Duration, o => o.MapFrom(y => y.consultation.Duration))
                .ForMember(x => x.description, o => o.MapFrom(y => y.consultation.description))
                .ForMember(x => x.type, o => o.MapFrom(y => y.consultation.type))
                .ForMember(x => x.InstructorId, o => o.MapFrom(y => y.consultation.InstructorId))               
                .ForMember(x => x.date, o => o.MapFrom(y => y.consultation.date.ToString("dd/MM/yyyy")));


            CreateMap<StudentConsultations, UserNameDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(y => y.Student.user.userName + " " + y.Student.user.LName));

            CreateMap<Consultation, LecturesForRetriveDTO>()
                .ForMember(x => x.InstructoruserName, o => o.MapFrom(y => y.instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.instructor.user.LName))
                .ForMember(x => x.StudentuserName, o => o.MapFrom(y => y.student.user.userName))
                .ForMember(x => x.StudentLName, o => o.MapFrom(y => y.student.user.LName));


            CreateMap<StudentCourse, ViewTheRequestOfJoindCourseDTO>()
                .ForMember(x => x.StudentName, o => o.MapFrom(y => y.Student.user.userName + " " + y.Student.user.LName))
                .ForMember(x => x.CourseName, o => o.MapFrom(y => y.Course.name))
                .ForMember(x => x.EnrollDate, o => o.MapFrom(y => y.EnrollDate.Date.ToString("dd/MM/yyyy")));
            
        }



        }
    }
