﻿using AutoMapper;
using courseProject.Controllers;
using courseProject.core.Models;
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
                .ForMember(x => x.ImageUrl, o => o.MapFrom(y => $"http://localhost:5134/{ y.ImageUrl}"));

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

            CreateMap<CourseForEditDTO, Course>();
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
        }
    }
}
