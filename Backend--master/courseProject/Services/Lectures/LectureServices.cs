using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Repository.GenericRepository;
using courseProject.ServiceErrors;
using ErrorOr;
using System.Net;

namespace courseProject.Services.Lectures
{
    public class LectureServices : ILectureServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CommonClass commonClass;

        public LectureServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            commonClass= new CommonClass();
        }

       

        public async Task<ErrorOr<IReadOnlyList<LecturesForRetriveDTO>>> GetAllLecturesByInstructorId(Guid instructorId)
        {
            var instructorFound = await unitOfWork.instructorRepositpry.GetEmployeeById(instructorId);
            if (instructorFound == null) return ErrorInstructor.NotFound;
            
            var GetLectures = await unitOfWork.instructorRepositpry.GetAllConsultationRequestByInstructorIdAsync(instructorId);
          
            var LecturesMapper = mapper.Map<IReadOnlyList<Consultation>, IReadOnlyList<LecturesForRetriveDTO>>(GetLectures);
            return LecturesMapper.ToErrorOr();
           
        }


        public async Task<ErrorOr<Created>> BookALecture(Guid studentId, DateTime date, string startTime, string endTime, BookALectureDTO bookALecture)
        {
          
            var student = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (student == null) return ErrorStudent.NotFound;
            if (!CommonClass.IsValidTimeFormat(startTime) || !CommonClass.IsValidTimeFormat(endTime) )
                return ErrorLectures.InvalidTime;
            TimeSpan StartTime = CommonClass.ConvertToTimeSpan(startTime);
            TimeSpan EndTime = CommonClass.ConvertToTimeSpan(endTime);


            if ((EndTime - StartTime) > TimeSpan.Parse("02:00") || (EndTime - StartTime) < TimeSpan.Parse("00:30"))
                return ErrorLectures.limitationTime;
            var CheckTime = await unitOfWork.instructorRepositpry.showifSelectedTimeIsAvilable(StartTime, EndTime, date);
            if (CheckTime.Count() == 0) return ErrorInstructor.NoInstructorAvailable;


            var consultation = mapper.Map<BookALectureDTO, Consultation>(bookALecture);
            consultation.StudentId = studentId;
            consultation.startTime = StartTime;
            consultation.endTime = EndTime;
            consultation.date = date;
            consultation.Duration = EndTime - StartTime;

            await unitOfWork.StudentRepository.BookLectureAsync(consultation);
            await unitOfWork.StudentRepository.saveAsync();
            var studentConsulation = mapper.Map<Consultation, StudentConsultations>(consultation);
            await unitOfWork.StudentRepository.AddInStudentConsulationAsync(studentConsulation);
            await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Created>> JoinToPublicLecture(Guid StudentId, Guid ConsultaionId)
        {
            var student = await unitOfWork.StudentRepository.getStudentByIdAsync(StudentId);
            if (student == null) return ErrorStudent.NotFound;
            var allConsultations = await unitOfWork.StudentRepository.GetAllPublicConsultationsAsync();
            if (!allConsultations.Any(x => x.Id == ConsultaionId)) return ErrorLectures.NotFound;
                 
            StudentConsultations studentConsultation = new StudentConsultations();
            studentConsultation.StudentId = StudentId;
            studentConsultation.consultationId = ConsultaionId;
            await unitOfWork.StudentRepository.AddInStudentConsulationAsync(studentConsultation);
            await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<IReadOnlyList<PublicLectureForRetriveDTO>>> GetAllConsultations(Guid studentId)
        {
            var getstudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getstudent == null) return ErrorStudent.NotFound;
            var allPublicConsultations = await unitOfWork.StudentRepository.GetAllConsultations();
            var publicConsulations = allPublicConsultations.DistinctBy(x => x.consultationId).ToList();
            var itsPrivateConsultations = await unitOfWork.StudentRepository.GetAllBookedPrivateConsultationsAsync(studentId);
           
            IReadOnlyList<PublicLectureForRetriveDTO>? lectureForRetrive = new List<PublicLectureForRetriveDTO>();
            lectureForRetrive = mapper.Map<IReadOnlyList<StudentConsultations>, IReadOnlyList<PublicLectureForRetriveDTO>>(publicConsulations);
            List<StudentConsultations>? allStudent = null;
            List<UserNameDTO>? allPublicStudents = null;
            foreach (var lecture in lectureForRetrive)
            {
                if (lecture.type.ToLower() == "public")
                {
                    allStudent = await unitOfWork.StudentRepository.GetAllStudentsInPublicConsulations(lecture.consultationId);

                    allPublicStudents = mapper.Map<List<StudentConsultations>, List<UserNameDTO>>(allStudent);
                    lecture.Students = allPublicStudents;
                }

            }
            var privateLectures = mapper.Map<IReadOnlyList<StudentConsultations>, IReadOnlyList<PublicLectureForRetriveDTO>>(itsPrivateConsultations);
            foreach (var lecture in privateLectures)
            {
                if (lecture.type.ToLower() == "private")
                {
                    var student = itsPrivateConsultations.Where(x => x.consultationId == lecture.consultationId).FirstOrDefault();
                    var studentmapper = mapper.Map<StudentConsultations, UserNameDTO>(student);
                    if (lecture.Students == null)
                    {
                        lecture.Students = new List<UserNameDTO>();
                    }
                    lecture.Students.Add(studentmapper);
                }
            }
            lectureForRetrive = lectureForRetrive.Concat(privateLectures).ToList();
            return lectureForRetrive.ToErrorOr();
        }

        public async Task<ErrorOr<LecturesForRetriveDTO>> GetConsultationById(Guid consultationId)
        {
            var getConsultation = await unitOfWork.StudentRepository.GetConsultationById(consultationId);
            if (getConsultation == null) return ErrorLectures.NotFound;
           
         
                var consultationMapper = mapper.Map<Consultation, LecturesForRetriveDTO>(getConsultation);
               return consultationMapper.ToErrorOr();
        
        }
    }
}
