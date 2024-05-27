using AutoMapper;
using courseProject.Common;
using courseProject.Controllers;
using courseProject.core.Models;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.Core.Models.DTO.StudentsDTO;

namespace courseProject.MappingProfile
{
    public class MappingForCourseInformation : Profile
    {
      //  private Common.IsNotDefaultClassForMapping IsNotDefaultClass;
        public MappingForCourseInformation()
        {
            //IsNotDefaultClass = new Common.IsNotDefaultClassForMapping();

            CreateMap<Course, CourseInformationDto>()
                .ForMember(x => x.InstructorName, o => o.MapFrom(y => y.Instructor.user.userName + " " + y.Instructor.LName))
                .ForMember(x => x.SubAdminName, o => o.MapFrom(y => y.SubAdmin.user.userName + " " + y.SubAdmin.LName))
                .ForMember(x => x.startDate, o => o.MapFrom(y => y.startDate.HasValue ? y.startDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.endDate, o => o.MapFrom(y => y.endDate.HasValue ? y.endDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.Deadline, o => o.MapFrom(y => y.Deadline.HasValue ? y.Deadline.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.ImageUrl}"));
                

            CreateMap<Course, CourseInfoForStudentsDTO>()
                .ForMember(x => x.InstructorName, o => o.MapFrom(y => y.Instructor.user.userName + " " + y.Instructor.LName))
                .ForMember(x => x.SubAdminName, o => o.MapFrom(y => y.SubAdmin.user.userName + " " + y.Instructor.LName))
                .ForMember(x => x.startDate, o => o.MapFrom(y => y.startDate.HasValue ? y.startDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.endDate, o => o.MapFrom(y => y.endDate.HasValue ? y.endDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.ImageUrl}"))
                .ForMember(x => x.isEnrolled, o => o.MapFrom(y => y.studentCourses.Any(x => x.isEnrolled)));

            CreateMap<Course, CourseAccreditDTO>()
                .ForMember(x => x.SubAdminFName, o => o.MapFrom(y => y.SubAdmin.user.userName))
                .ForMember(x => x.SubAdminLName, o => o.MapFrom(y => y.SubAdmin.LName))
                .ForMember(x => x.InstructorFName, o => o.MapFrom(y => y.Instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.Instructor.LName))
                .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.ImageUrl}"));


            CreateMap<CourseForCreateDTO, Course>();
            CreateMap<CourseForCreateDTO, Request>()
                .ForMember(x => x.name , o=>o.MapFrom(y=>y.name));
            CreateMap<Request, Course>()
                .ForMember(x => x.requestId, o => o.MapFrom(y => y.Id));

            CreateMap<CourseMaterialDTO, CourseMaterial>();

            // CreateMap<CourseForEditDTO, Course>();

            CreateMap<CourseForEditDTO, Course>()
            //.ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) => (srcMember != null || srcMember != 0)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping .IsNotDefault(srcMember)));
            ;


            CreateMap<TaskDTO, CourseMaterial>();

            CreateMap<FileDTO, CourseMaterial>();
            CreateMap<AnnouncementDTO, CourseMaterial>();
            CreateMap<LinkDTO, CourseMaterial>();
            CreateMap<CourseMaterial, TaskForRetriveDTO>()
                .ForMember(x => x.pdfUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.pdfUrl}"));
            CreateMap<CourseMaterial, FileForRetriveDTO>()
                .ForMember(x => x.pdfUrl, o => o.MapFrom(y => $"http://localhost:5134/{y.pdfUrl}"));
            CreateMap<CourseMaterial, AnnouncementForRetriveDTO>();
            CreateMap<CourseMaterial, LinkForRetriveDTO>();
            //  .ForMember(x=>x.pdf , o=>o.MapFrom(y=>y.pdfUrl));


            CreateMap<StudentCourseDTO, StudentCourse>();
            CreateMap<SubmissionsDTO, Student_Task_Submissions>();

            CreateMap<StudentCustomCourseDTO, Request>();
            CreateMap<Request, CustomCourseForRetriveDTO>()
                .ForMember(x => x.StudentFName, o => o.MapFrom(y => y.Student.user.userName))
                .ForMember(x => x.StudentLName, o => o.MapFrom(y => y.Student.LName))
              //  .ForMember(x => x.description, o => o.MapFrom(y => y.Student.Consultation.ToString()))
                .ForMember(x => x.startDate, o => o.MapFrom(y => y.startDate.HasValue ? y.startDate.Value.ToString("dd/MM/yyyy" ) :null))
                .ForMember(x => x.endDate, o => o.MapFrom(y => y.endDate.HasValue ? y.endDate.Value.ToString("dd/MM/yyyy") : null));

            CreateMap<EditCourseAfterAccreditDTO, Course>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            

        }
    }
}
