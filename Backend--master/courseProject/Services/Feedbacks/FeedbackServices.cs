using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.Repository.GenericRepository;
using courseProject.ServiceErrors;
using ErrorOr;
using System.Net;

namespace courseProject.Services.Feedbacks
{
    public class FeedbackServices : IFeedbackServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FeedbackServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        

        public async Task<ErrorOr<Created>> AddInstructorFeedback(Guid studentId, Guid InstructorId, FeedbackDTO Feedback)
        {
            var getStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getStudent == null) return ErrorStudent.NotFound;
            

            var getInstructors = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(InstructorId);
            if (getInstructors == null) return ErrorInstructor.NotFound;
         

            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "instructor-feedback";
            feddbackMapper.StudentId = studentId;
            feddbackMapper.InstructorId = InstructorId;
            await unitOfWork.StudentRepository.addFeedbackAsync(feddbackMapper);
            var success = await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }


        public async Task<ErrorOr<Created>> AddCourseFeedback(Guid studentId, Guid courseId, FeedbackDTO Feedback)
        {
            var getStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getStudent == null) return ErrorStudent.NotFound;

            var getCourse = await unitOfWork.StudentRepository.GetAllCoursesForStudentAsync(studentId);
            if (!getCourse.Any(x => x.courseId == courseId))
                return ErrorCourse.UnavailableCourse;

            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "course-feedback";
            feddbackMapper.StudentId = studentId;
            feddbackMapper.CourseId = courseId;
            await unitOfWork.StudentRepository.addFeedbackAsync(feddbackMapper);
            var success = await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Created>> AddGeneralFeedback(Guid studentId, FeedbackDTO Feedback)
        {
            var getStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getStudent == null) return ErrorStudent.NotFound;
            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "general-feedback";
            feddbackMapper.StudentId = studentId;
            await unitOfWork.StudentRepository.addFeedbackAsync(feddbackMapper);
            await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }

        public async Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllGeneralFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetFeedbacksByTypeAsync("general-feedback");
          
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            return feedbackMapper;
        }

        public async Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllInstructorFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetFeedbacksByTypeAsync("instructor-feedback");
           
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            return feedbackMapper;
        }

        public async Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllCourseFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetFeedbacksByTypeAsync("course-feedback");
            
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            return feedbackMapper;
        }

        public async Task<IReadOnlyList<AllFeedbackForRetriveDTO>> GetAllFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetAllFeedbacksAsync();
          
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<AllFeedbackForRetriveDTO>>(getFeedback);
            return feedbackMapper;
        }

        public async Task<ErrorOr<FeedbackForRetriveDTO>> GetFeedbackById(Guid id)
        {
            var getFeedbacks = await unitOfWork.StudentRepository.GetAllFeedbacksAsync();
            if (!getFeedbacks.Any(x => x.Id == id)) return ErrorFeedback.NotFound;
            
            var getAFeedback = await unitOfWork.StudentRepository.GetFeedbackByIdAsync(id);
            var feedbackMapper = mapper.Map<Feedback, FeedbackForRetriveDTO>(getAFeedback);
            return feedbackMapper;        
    }


    }
}
