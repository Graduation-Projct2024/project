using AutoMapper;
using courseProject.Controllers;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;

namespace courseProject.MappingProfile
{
    public class MappingForCourseInformation : Profile
    {
        public MappingForCourseInformation()
        {
           

            CreateMap<Course, CourseInformationDto>()
                .ForMember(x => x.InstructorName  , o=>o.MapFrom(y=>y.Instructor.user.userName))
                .ForMember(x => x.SubAdminName, o => o.MapFrom(y => y.SubAdmin.user.userName))
                ;

            CreateMap<Course, CourseAccreditDTO>()
                .ForMember(x => x.SubAdminFName, o => o.MapFrom(y => y.SubAdmin.user.userName))
                .ForMember(x=>x.SubAdminLName , o=>o.MapFrom(y=>y.SubAdmin.LName))
                .ForMember(x => x.InstructorFName, o => o.MapFrom(y => y.Instructor.user.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.Instructor.LName));

            CreateMap<CourseForCreateDTO, Course>();
            CreateMap<CourseForCreateDTO, Request>()
                .ForMember(x => x.name , o=>o.MapFrom(y=>y.name));
            CreateMap<Request, Course>()
                .ForMember(x => x.requestId, o => o.MapFrom(y => y.Id));

            CreateMap<CourseMaterialDTO, CourseMaterial>();

            CreateMap<CourseForEditDTO, Course>();
            CreateMap<TaskDTO, CourseMaterial>();

            CreateMap<FileDTO, CourseMaterial>();
            CreateMap<AnnouncementDTO, CourseMaterial>();
            CreateMap<LinkDTO, CourseMaterial>();
            CreateMap<CourseMaterial, TaskForRetriveDTO>();
            CreateMap<CourseMaterial, FileForRetriveDTO>();
            CreateMap<CourseMaterial, AnnouncementForRetriveDTO>();
            CreateMap<CourseMaterial, LinkForRetriveDTO>();
            //  .ForMember(x=>x.pdf , o=>o.MapFrom(y=>y.pdfUrl));


            CreateMap<StudentCourseDTO, StudentCourse>();
        }
    }
}
