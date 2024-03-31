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
            var Students = await studentRepo.GetAllStudentsAsync();

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
            var students = await studentRepo.GetAllStudentsForContactAsync();
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
        public async Task<ActionResult<ApiResponce>> BoolLectureByStudent(int studentId , DateTime date , string startTime , string endTime , [FromForm] BookALectureDTO bookALecture)
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
            if(await unitOfWork.StudentRepository.saveAsync() > 0)
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

    }


    }

