using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using courseProject.Repository.GenericRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using courseProject.Common;
using courseProject.core.Models;
using AutoMapper.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsContraller : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository1<Student> studentRepo;
        private readonly IMapper mapper;
        protected ApiResponce response;
        private Common.CommonClass CommonClass;

        public StudentsContraller(IUnitOfWork unitOfWork, IGenericRepository1<Student> StudentRepo , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            studentRepo = StudentRepo;
            this.mapper = mapper;
            response=new ApiResponce();
            CommonClass = new Common.CommonClass();
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
       
        public async Task <ActionResult<IEnumerable<Student>>> GetAllStudentsAsync()
        {
            var Students = await unitOfWork.StudentRepository.GetAllStudentsAsync();

            if(Students == null)
            {
                return NotFound();
            }
            
            var mappedStudentDTO = mapper.Map<IEnumerable<Student>, IEnumerable<StudentsInformationDto>>(Students);

            return Ok(mappedStudentDTO);
        }


        [HttpGet("contact")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IReadOnlyList<Student>>> GetAllStudentsForContactAsync()
        {
            var students = await unitOfWork.StudentRepository.GetAllStudentsForContactAsync();
            if (students == null)
            {
                return NotFound();
            }
            var mapperStudents = mapper.Map<IReadOnlyList<Student>, IReadOnlyList<ContactDto>>(students);
            var updatedStudents = mapperStudents.Select(model =>
            {
                model.ImageUrl = $"http://localhost:5134/{model.ImageUrl}";
                return model;
            }).ToList();
            return Ok(mapperStudents);

        }


        [HttpPost("EnrollInCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EnrollInCourseAsync (StudentCourseDTO studentCourseDTO)
        {
            if (studentCourseDTO == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the inputs is null" };
                return NotFound(response);
            }
           // var studentFound = d
            if ( !ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var mapped = mapper.Map<StudentCourseDTO, StudentCourse>(studentCourseDTO);
            await unitOfWork.StudentRepository.EnrollCourse(mapped);
            var success = await unitOfWork.StudentRepository.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Result = studentCourseDTO;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }


        [HttpGet("GetAllEnrolledCoursesForAStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetEnrolledCourses(int studentid)
        {
            if (studentid <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(response);
            }
            var Studentfound = await unitOfWork.StudentRepository.GetAllCoursesForStudentAsync(studentid);
            if (Studentfound.Count() == 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { $"The Student Of Id = { studentid} Does Not Enroll Any Course " };
                return NotFound(response);
            }
            var courseFound =  Studentfound.Select(x => x.Course).ToList();
            CommonClass.EditImageInFor(courseFound , null);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = Studentfound;
            return Ok(response);
        }

        [HttpPost("AddTaskSubmission")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<ApiResponce>> AddTaskByStudent(int Studentid , int taskid ,[FromForm] SubmissionsDTO submissions)
        {
            if (Studentid<=0 || taskid<=0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(response);
            }
            var taskFound=await unitOfWork.materialRepository.GetMaterialByIdAsync(taskid);
            var studentFound=await unitOfWork.UserRepository.ViewProfileAsync(Studentid , "student");
            if(studentFound==null ||taskFound==null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "The StudentId OR/AND TaskId Not Found" };
                return NotFound(response);
            }
            if( ! submissions.GetType()
                 .GetProperties() 
                 .Select(pi => pi.GetValue(submissions)) 
                 .Any(value => value != null) 
              )
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "You haven't added anything" };
                return BadRequest(response);
            }
            var student_Task = mapper.Map<SubmissionsDTO, Student_Task_Submissions>(submissions);
            student_Task.StudentId = Studentid;
            student_Task.TaskId=taskid;
            if (student_Task.pdf != null)
            {
                student_Task.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(student_Task.pdf);
            }           
            await unitOfWork.StudentRepository.SubmitTaskAsync(student_Task);
            var success = await unitOfWork.StudentRepository.saveAsync();
            if (submissions.pdf != null)
            {
                student_Task.pdfUrl = "http://localhost:5134/" + student_Task.pdfUrl;
            }
            if (success > 0)
            {
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Result = student_Task;
                return Ok(response);
            }
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(response);
        }


        [HttpGet("GetCourseParticipants")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetCourseParticipants(int Courseid)
        {
            if (Courseid <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(response);
            }
            var courseFound = await unitOfWork.CourseRepository.GetCourseByIdAsync(Courseid);
            if(courseFound == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "The Course Id is Not Found" };
                return NotFound(response);
            }
            if (courseFound.status.ToLower() == "accredit")
            {
                var GetStudents = await unitOfWork.StudentRepository.GetAllStudentsInTheSameCourseAsync(Courseid);
                if(GetStudents == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMassages = new List<string>() { "This Course Does Not Has Any Student!" };
                    return NotFound(response);
                }
                var StudentMapper = mapper.Map<IReadOnlyList< Student>,IReadOnlyList<StudentsInformationDto>>(GetStudents);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = StudentMapper;
                return Ok(response);
            }
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMassages = new List<string>() { "This Course Does Not Accredit Yet!" };
            return BadRequest(response);
        }


        [HttpPost("RequestToCreateCustomCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> RequestToCreateACustomCourse (int studentid  , [FromForm] StudentCustomCourseDTO studentCustomCourse)
        {
            if(studentid <=0 )
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("the id is less or equal 0 ");
                return BadRequest(response);    
            }
            if(!studentCustomCourse.GetType()
                 .GetProperties()
                 .Select(s => s.GetValue(studentCustomCourse))
                 .Any(value => value != null))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("No input is added ");
                return BadRequest(response);
            }
            var allStudents = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            if(! allStudents.Any(x=>x.StudentId == studentid))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add($"The student with id ={studentid} is not found");
                return BadRequest(response);
            }
            var customCourseMapper = mapper.Map< StudentCustomCourseDTO , Request>(studentCustomCourse);
            customCourseMapper.satus = "custom-course";
            customCourseMapper.StudentId = studentid;
            var adminid = await  unitOfWork.UserRepository.GetAdminId();
            customCourseMapper.AdminId = adminid.UserId;
            await unitOfWork.SubAdminRepository.CreateRequest(customCourseMapper);
            if(await unitOfWork.CourseRepository.saveAsync() > 0)
            {
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Result = studentCustomCourse;
                return Ok(response);
            }
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(response);
        }


        [HttpPost("BookALecture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> BooKLectureByStudent(int studentId , DateTime date , string startTime , string endTime , [FromForm] BookALectureDTO bookALecture)
        {  
            
            if(!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            if(! bookALecture.GetType().GetProperties()
                .Select(x => x.GetValue(bookALecture))
                 .Any(value => value != null))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("No input is added ");
                return BadRequest(response);
            }
            var allStudents = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            if (!allStudents.Any(x => x.StudentId == studentId))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add($"The student with id ={studentId} is not found");
                return BadRequest(response);
            }
            if (!CommonClass.IsValidTimeFormat(startTime) || !CommonClass.IsValidTimeFormat(endTime) || !CommonClass.IsValidTimeFormat(bookALecture.Duration))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("The Input Time Has An Error");
                return BadRequest(response);
            }
            TimeSpan StartTime = CommonClass.ConvertToTimeSpan(startTime);
            TimeSpan EndTime = CommonClass.ConvertToTimeSpan(endTime);
            TimeSpan duration = CommonClass.ConvertToTimeSpan(bookALecture.Duration);
            if ((EndTime- StartTime) < duration)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("The Duration Input Is Greater Than The Difference Between Start And End Time !, Edit It");
                return BadRequest(response);
            }
            
            var consultation = mapper.Map<BookALectureDTO , Consultation>(bookALecture);
            consultation.StudentId = studentId;
            consultation.startTime = StartTime;
            consultation.endTime= EndTime;
            consultation.date = date;

            await unitOfWork.StudentRepository.BookLectureAsync(consultation);
            var success1 = await unitOfWork.StudentRepository.saveAsync();
            var studentConsulation = mapper.Map<Consultation , StudentConsultations>(consultation);
            await unitOfWork.StudentRepository.AddInStudentConsulationAsync(studentConsulation);
            var success2 =await unitOfWork.StudentRepository.saveAsync();
            if(success1 > 0 && success2 > 0)
            {
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Result = bookALecture;
                return Ok(response);
            }
            response.IsSuccess = false;
            response.StatusCode=HttpStatusCode.BadRequest;
            return BadRequest(response);

        }


        [HttpPost("JoinToPublicLecture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> JoinToAPublicLecture(int StudentId , int ConsultaionId)
        {
            if (StudentId <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("The Studet Id is less or equal 0");
                return BadRequest(response);
            }
            if (ConsultaionId <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("The Consultation Id is less or equal 0");
                return BadRequest(response);
            }
            var allStudents = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            if (!allStudents.Any(x=>x.StudentId==StudentId))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add($"The student with id ={StudentId} is not found");
                return NotFound(response);
            }
            var allConsultations = await unitOfWork.StudentRepository.GetAllPublicConsultationsAsync();
            if(! allConsultations.Any(x=>x.Id==ConsultaionId))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add($"The Consultation with id ={ConsultaionId} is not found");
                return NotFound(response);
            }
            StudentConsultations studentConsultation=new StudentConsultations();
            studentConsultation.StudentId = StudentId;
            studentConsultation.consultationId = ConsultaionId;
             await unitOfWork.StudentRepository.AddInStudentConsulationAsync(studentConsultation);
            if(await unitOfWork.StudentRepository.saveAsync() > 0)
            {
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = studentConsultation;
                return Ok(response);
            }
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMassages.Add("An error occured , the dat is not saved , return enter the correct data ");
            return BadRequest(response);

        }

        [HttpPost("GetAllConsultations")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllConsultation(int studentId)
        {
            if (studentId <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("The Studet Id is less or equal 0");
                return BadRequest(response);
            }
            var allStudents = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            if(!allStudents.Any(x => x.StudentId == studentId))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add($"The student with id ={studentId} is not found");
                return NotFound(response);
            }
            var allPublicConsultations = await unitOfWork.StudentRepository.GetAllConsultations();
           var publicConsulations= allPublicConsultations.DistinctBy(x => x.consultationId).ToList(); 
           // allPublicConsultations.GroupBy(StudentConsultations => StudentConsultations.consultationId);
            var itsPrivateConsultations = await unitOfWork.StudentRepository.GetAllBookedPrivateConsultationsAsync(studentId);
          // var allStudent = await unitOfWork.StudentRepository.GetAllStudentsInPublicConsulations(3);
            if (allPublicConsultations.Count() == 0 && itsPrivateConsultations.Count()==0)
            {
                response.IsSuccess = true;
                response.StatusCode=HttpStatusCode.NoContent;
                response.ErrorMassages.Add("There is not have any consultaion yet");
                return Ok(response);
            }
            //IReadOnlyList<PublicLectureForRetriveDTO>? allLectures =new List<PublicLectureForRetriveDTO>();
            IReadOnlyList< PublicLectureForRetriveDTO>? lectureForRetrive=new List<PublicLectureForRetriveDTO>() ;
            //foreach (var consultation in allPublicConsultations)
            //{
            //    allLectures = mapper.Map<StudentConsultations, PublicLectureForRetriveDTO>(consultation);
            //    allLectures.StudentuserName.Add(consultation.Student.user.userName);
            //    allLectures.StudentLName.Add(consultation.Student.LName);
            //    allLectures.Add(lectureForRetrive);
            //}
            lectureForRetrive = mapper.Map<IReadOnlyList< StudentConsultations>,IReadOnlyList< PublicLectureForRetriveDTO>>(publicConsulations);
            List<StudentConsultations>? allStudent=null;
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
           // lectureForRetrive= lectureForRetrive.Concat(
             var privateLectures=   mapper.Map<IReadOnlyList<StudentConsultations>, IReadOnlyList<PublicLectureForRetriveDTO>>(itsPrivateConsultations) ;
            foreach (var lecture in privateLectures)
            {
                if (lecture.type.ToLower() == "private")
                {
                    var student = itsPrivateConsultations.Where(x=>x.consultationId==lecture.consultationId).FirstOrDefault();
                    var studentmapper = mapper.Map<StudentConsultations, UserNameDTO>(student);
                    if (lecture.Students == null)
                    {
                        lecture.Students = new List<UserNameDTO>();
                    }
                    lecture.Students.Add ( studentmapper);
                }

            }
            lectureForRetrive = lectureForRetrive.Concat(privateLectures).ToList();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = lectureForRetrive;
            return Ok(response);
        }


        [HttpPost("GetConsultationById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAConsultationById(int consultationId)
        {
            if (consultationId <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode=HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("The lecture Id is less or equal 0");
                return BadRequest(response);
            }
            var getConsultation = await unitOfWork.StudentRepository.GetConsultationById(consultationId);
            if (getConsultation == null)
            {
                response.IsSuccess = false;
                response.StatusCode=HttpStatusCode.BadRequest;
                response.ErrorMassages.Add($"The lecture with id = {consultationId} is not found ");
                return NotFound(response);
            }
            try
            {
                var consultationMapper = mapper.Map<Consultation, LecturesForRetriveDTO>(getConsultation);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = consultationMapper;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("An error has occured ");
                return BadRequest(response);
            }
        }


        [HttpPost("AddInstructorFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddInstructorFeedback(int studentId, int InstructorId, FeedbackDTO Feedback)
        {
            var getStudents = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            if (!getStudents.Any(x => x.StudentId == studentId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add($"this id {studentId} is not in student table");
                return BadRequest(response);
            }

            var getInstructors = await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();
            if (!getInstructors.Any(x => x.InstructorId == InstructorId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add($"this id {InstructorId} is not in instructor table");
                return BadRequest(response);
            }

            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "instructor-feedback";
            feddbackMapper.StudentId = studentId;
            feddbackMapper.InstructorId = InstructorId;
            await unitOfWork.StudentRepository.addFeedbackAsync(feddbackMapper);
            var success = await unitOfWork.StudentRepository.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = feddbackMapper;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }




        [HttpPost("AddCourseFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddCourseFeedback(int studentId, int courseId, FeedbackDTO Feedback)
        {
            var getStudents = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            if (!getStudents.Any(x => x.StudentId == studentId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add($"this id {studentId} is not in student table");
                return BadRequest(response);
            }

            var getCourse = await unitOfWork.StudentRepository.GetAllCoursesForStudentAsync(studentId);
            if (!getCourse.Any(x => x.courseId == courseId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add($"this course with id = {courseId} is not available to you");
                return BadRequest(response);
            }

            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "course-feedback";
            feddbackMapper.StudentId = studentId;
            feddbackMapper.CourseId = courseId;
            await unitOfWork.StudentRepository.addFeedbackAsync(feddbackMapper);
            var success = await unitOfWork.StudentRepository.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = feddbackMapper;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }


        [HttpPost("AddGeneralFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddGeneralFeedback(int studentId, FeedbackDTO Feedback)
        {
            var getStudents = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            if (!getStudents.Any(x => x.StudentId == studentId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add($"this id {studentId} is not in student table");
                return BadRequest(response);
            }
            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "general-feedback";
            feddbackMapper.StudentId = studentId;          
            await unitOfWork.StudentRepository.addFeedbackAsync(feddbackMapper);
            var success = await unitOfWork.StudentRepository.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = feddbackMapper;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }


        [HttpGet("GetAllGeneralFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllGeneralFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetFeedbacksByTypeAsync("general - feedback");
            if (getFeedback.Count() == 0)
            {
                response.StatusCode=HttpStatusCode.NotFound;
                response.ErrorMassages.Add("There is not any feedback yet");
                return Ok(response);
            }
            var feedbackMapper= mapper.Map<IReadOnlyList<Feedback> ,IReadOnlyList<FeedbackForRetriveDTO>> (getFeedback);
            response.IsSuccess = true;
            response.StatusCode= HttpStatusCode.OK;
            response.Result=feedbackMapper;
            return Ok(response);
        }


        [HttpGet("GetAllInstructorFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllInstructorFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetFeedbacksByTypeAsync("instructor - feedback");
            if (getFeedback.Count() == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add("There is not any feedback yet");
                return Ok(response);
            }
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = feedbackMapper;
            return Ok(response);
        }

        [HttpGet("GetAllCourseFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllCourseFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetFeedbacksByTypeAsync("course - feedback");
            if (getFeedback.Count() == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add("There is not any feedback yet");
                return Ok(response);
            }
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = feedbackMapper;
            return Ok(response);
        }


        [HttpGet("GetAllFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllFeedback()
        {
            var getFeedback = await unitOfWork.StudentRepository.GetAllFeedbacksAsync();
            if (getFeedback.Count() == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add("There is not any feedback yet");
                return Ok(response);
            }
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<AllFeedbackForRetriveDTO>>(getFeedback);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = feedbackMapper;
            return Ok(response);
        }



        [HttpGet("GetFeedbackById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetFeedbackById(int id)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode=HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("The id is less or equal 0 ");
                return BadRequest(response);
            }
            var getFeedbacks = await unitOfWork.StudentRepository.GetAllFeedbacksAsync();
            if (getFeedbacks.Any(x=>x.Id==id))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add($"The feedback with id ={id} is not found ");
                return Ok(response);
            }
            var getAFeedback = await unitOfWork.StudentRepository.GetFeedbackByIdAsync(id);
            var feedbackMapper = mapper.Map<Feedback, FeedbackForRetriveDTO>(getAFeedback);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = feedbackMapper;
            return Ok(response);
        }

        //[HttpGet("GetAllLecturesByStudentId")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult<ApiResponce>> GetALllLecturesForStudet(int StudentId)
        //{
        //    if (StudentId <= 0)
        //    {
        //        response.IsSuccess = false;
        //        response.StatusCode = HttpStatusCode.BadRequest;
        //        response.ErrorMassages.Add("The Studet id is less or equal 0");
        //        return BadRequest(response);
        //    }
        //    var getLectures = await unitOfWork.StudentRepository.GetAllLectureByStudentIdAsync(StudentId);
        //    if (getLectures.Count() == 0)
        //    {
        //        response.IsSuccess = true;
        //        response.StatusCode= HttpStatusCode.NoContent;
        //        response.ErrorMassages.Add("There is no lecture");
        //        return Ok(response);
        //    }

        //}

    }


    }

