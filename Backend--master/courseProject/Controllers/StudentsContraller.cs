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
            CommonClass.EditImageInFor(courseFound);
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

            if(submissions == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "You haven't added anything" };
                return BadRequest(response);
            }
            var student_Task = mapper.Map<SubmissionsDTO, Student_Task_Submissions>(submissions);
          //  Student_Task_Submissions student_Task=null;
            student_Task.StudentId = Studentid;
            student_Task.TaskId=taskid;
            // student_Task.description = submissions.description;
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


        ////[HttpPost("CreateStudent")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult<Student>> CreateStudent(RegistrationRequestDTO model)
        //{

        //    var modelMapped = mapper.Map<Student>(model);
        //    using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //           await unitOfWork.UserRepository.RegisterAsync(model);
        //            var success1 = await unitOfWork.StudentRepository.saveAsync();

        //            await unitOfWork.StudentRepository.CreateStudentAccountAsync(modelMapped);
        //            var success2 = await unitOfWork.StudentRepository.saveAsync();


        //            if (success1 > 0 && success2 > 0)
        //            { 
        //                await transaction.CommitAsync();
        //                response.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
        //                response.IsSuccess = true;
        //                response.Result = model;
        //                return Ok(response);
        //            }

        //            return BadRequest(response);
        //        }
        //        catch (Exception ex)
        //        {
        //            await transaction.RollbackAsync();


        //            response.StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest;
        //            response.IsSuccess = false;

        //            return BadRequest(response);
        //        }

        //    }
    }


    }

