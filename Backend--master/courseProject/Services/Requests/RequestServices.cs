using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Repository.GenericRepository;
using courseProject.ServiceErrors;
using ErrorOr;
using System.Net;

namespace courseProject.Services.Requests
{
    public class RequestServices : IRequestServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RequestServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<ViewTheRequestOfJoindCourseDTO>> GetAllRequestToJoinCourses()
        {
            var getRequests = await unitOfWork.SubAdminRepository.getAllRequestToJoindCourseAsync();
           
            var requestMapper = mapper.Map<IReadOnlyList<StudentCourse>, IReadOnlyList<ViewTheRequestOfJoindCourseDTO>>(getRequests);
            return requestMapper;
        }

        public async Task<ErrorOr<Created>> RequestToCreateCustomCourse(Guid studentid, StudentCustomCourseDTO studentCustomCourse)
        {
            var foundStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentid);
            if (foundStudent == null) return ErrorStudent.NotFound;
           
            var customCourseMapper = mapper.Map<StudentCustomCourseDTO, Request>(studentCustomCourse);
            customCourseMapper.satus = "custom-course";
            customCourseMapper.StudentId = studentid;
          
            await unitOfWork.SubAdminRepository.CreateRequest(customCourseMapper);
            await unitOfWork.CourseRepository.saveAsync();
            return Result.Created;
        }
    }
}
