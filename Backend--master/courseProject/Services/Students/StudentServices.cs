using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Repository.GenericRepository;
using courseProject.ServiceErrors;
using ErrorOr;
using System.Net;

namespace courseProject.Services.Students
{
    public class StudentServices : IStudentServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
      //  private readonly CommonClass commonClass;

        public StudentServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
           // commonClass = new CommonClass();
        }

        public async Task<IReadOnlyList<StudentsInformationDto>> GetAllStudents()
        {
            var Students = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            foreach(var student in Students)
            {
                if (student.user.ImageUrl != null)
                {
                    student.user.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(student.user.ImageUrl);
                }
            }
          //  CommonClass.EditImageInStudents(Students);
            var mappedStudentDTO = mapper.Map<IReadOnlyList<Student>, IReadOnlyList<StudentsInformationDto>>(Students);
            return mappedStudentDTO;
        }

        //public async Task<IReadOnlyList<ContactDto>> GetAllStudentsForContact()
        //{
        //    var students = await unitOfWork.StudentRepository.GetAllStudentsForContactAsync();           
        //    var mapperStudents = mapper.Map<IReadOnlyList<Student>, IReadOnlyList<ContactDto>>(students);
        //    var updatedStudents = mapperStudents.Select(model =>
        //    {
        //        model.ImageUrl = $"http://localhost:5134/{model.ImageUrl}";
        //        return model;
        //    }).ToList();
        //   return updatedStudents;
        //}

        public async Task<ErrorOr<IReadOnlyList<StudentsInformationDto>>> GetCourseParticipants(Guid courseId)
        {
            var courseFound = await unitOfWork.CourseRepository.getAccreditCourseByIdAcync(courseId);
            if (courseFound == null) return ErrorCourse.NotFound;
            var GetStudents = await unitOfWork.StudentRepository.GetAllStudentsInTheSameCourseAsync(courseId);
            var StudentMapper = mapper.Map<IReadOnlyList<Student>, IReadOnlyList<StudentsInformationDto>>(GetStudents);
            return StudentMapper.ToErrorOr();
                
        }
    }
}
