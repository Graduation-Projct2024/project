using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using System.Net;

namespace courseProject.Services.StudentCourses
{
    public class StudentCoursesServices : IStudentCoursesServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public StudentCoursesServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

       

        public async Task<ErrorOr<Created>> EnrollInCourse(StudentCourseDTO studentCourseDTO)
        {
            
                var foundStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentCourseDTO.StudentId);
            if (foundStudent == null) return ErrorStudent.NotFound;
                var course = await unitOfWork.CourseRepository.GetCourseByIdAsync(studentCourseDTO.courseId);
            if(course == null) return ErrorCourse.NotFound;
                var studnetNumber = await unitOfWork.CourseRepository.GetNumberOfStudentsInTHeCourseAsync(studentCourseDTO.courseId);
            if (studnetNumber >= course.limitNumberOfStudnet)
                return ErrorCourse.fullCourse;
                var mapped = mapper.Map<StudentCourseDTO, StudentCourse>(studentCourseDTO);
                await unitOfWork.StudentRepository.EnrollCourse(mapped);
                 await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
            }


        public async Task<ErrorOr<Updated>> ApprovelToJoinCourse(Guid courseId, Guid studentId, string status)
        {
            var getStudentCourse = await unitOfWork.StudentRepository.GetFromStudentCourse(courseId, studentId);
            if (getStudentCourse == null) return ErrorStudentCourse.NotFound;
            
            Expression<Func<StudentCourse, string>> path = x => x.status;
            var patchDocument = new JsonPatchDocument<StudentCourse>();
            patchDocument.Replace(path, status);
            getStudentCourse.status = status;

            await unitOfWork.CourseRepository.UpdateStudentCourse(getStudentCourse);
            var studentEmail = (await unitOfWork.UserRepository.getUserByIdAsync(studentId)).email;
            string studentName = (await unitOfWork.UserRepository.ViewProfileAsync(studentId, "student")).userName;
            var courseName = (await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId)).name;
            if (status.ToLower() == "reject")
            {
                await unitOfWork.StudentRepository.RemoveTheRejectedRequestToJoinCourse(getStudentCourse);
                await unitOfWork.EmailService.SendEmailAsync(studentEmail , "Join Course" , $"Dear {studentName} ,Your request to join the course {courseName} has been Rejected");
            }
            await unitOfWork.EmailService.SendEmailAsync(studentEmail, "Join Course", $"Dear {studentName} ,Your request to join the course {courseName} has been Accepted");

            await unitOfWork.CourseRepository.saveAsync();

            return Result.Updated;
        }

        
    }
    }

