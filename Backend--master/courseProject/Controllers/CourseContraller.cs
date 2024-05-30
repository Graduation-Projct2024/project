using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using courseProject.Repository.GenericRepository;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using System;
using static System.Net.WebRequestMethods;
using courseProject.Common;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.EventsDTO;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Services.Courses;
using courseProject.Validations;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseContraller : ControllerBase
    {
        private readonly ICourseServices courseServices;
        private readonly IUnitOfWork unitOfWork;
        private readonly projectDbContext dbContext;
        private readonly IGenericRepository1<Course> courseRepo;
        private readonly IGenericRepository1<Request> requestRepo;
        private readonly IMapper mapper;
        protected ApiResponce responce;

        //  private Request request;

        public CourseContraller(ICourseServices courseServices, IUnitOfWork unitOfWork, projectDbContext dbContext, IGenericRepository1<Course> CourseRepo, IGenericRepository1<Request> RequestRepo, IMapper mapper)
        {
            this.courseServices = courseServices;
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
            courseRepo = CourseRepo;
            requestRepo = RequestRepo;
            this.mapper = mapper;
            responce = new ApiResponce();
        
        }

        [HttpGet("GetAllAccreditCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        
        //get all accredits courses
        public async Task<ActionResult<ApiResponce>> GetAllCoursesAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var getCourses = await courseServices.GetAllCourses();
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInformationDto>>(getCourses);          
            responce.Result = (Pagination<CourseInformationDto>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            return Ok(responce);
        }

        [HttpGet("GetAllCoursesToStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]

        //this is to retrive all courses to students to enroll in
        public async Task<ActionResult<ApiResponce>> GetAllCoursesToStudent(Guid studentId, [FromQuery] PaginationRequest paginationRequest)
        {

                var courses = await courseServices.GetAllCoursesToStudent(studentId);
                var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInfoForStudentsDTO>>(courses);
            return new ApiResponce { Result = (Pagination<CourseInfoForStudentsDTO>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result };
                 
           
        }

        [HttpGet("GetAllCoursesForAccredit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        //get all courses
        public async Task<ActionResult<ApiResponce>> GetAllCoursesForAccreditAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var courses = await courseServices.GetAllCoursesForAccreditAsync();
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseAccreditDTO>>(courses);
            return new ApiResponce { Result = (Pagination<CourseAccreditDTO>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result };           
            
        }



        [HttpPost("CreateCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]

        // a create course , created by subAdmin or main subAdmin
        public async Task<ActionResult<ApiResponce>> createCourse([FromForm] CourseForCreateDTO model, Guid? StudentId)
        {
         

            var courseMapped = mapper.Map<Course>(model);
            var requestMapped = mapper.Map<Request>(model);

            var createCourse = await courseServices.createCourse(courseMapped, requestMapped, StudentId);
            if (createCourse.IsError) return NotFound( new ApiResponce { ErrorMassages =  createCourse.FirstError.Description });
            return new ApiResponce { Result="The Course Is Created Successfully"};
            

        }


        


        [HttpPatch("accreditCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]

        // this to change the status of courses to reject or accredit
        public async Task<ActionResult<ApiResponce>> EditCourseStatus(Guid courseId, string Status)
        {
            var updateStatus = await courseServices.accreditCourse(courseId, Status);
            if (updateStatus.IsError) return NotFound(new ApiResponce
            {
                ErrorMassages = updateStatus.FirstError.Description
            }) ;
            return Ok(new ApiResponce
            {
                Result = $"The Course is {Status}"
            });
        }


        [HttpPut("EditOnCourseAfterAccredit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResponce>> EditOnCourseAfterAccreditByAdmin(Guid courseId, [FromForm] EditCourseAfterAccreditDTO editedCourse)
        {

            var courseService = await courseServices.EditOnCourseAfterAccredit(courseId, editedCourse);
            responce.Result = courseService;
            if (courseService.IsError == true) responce.ErrorMassages = courseService.FirstError.Description;
            return Ok(responce);

        }




        [HttpGet("GetCourseById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        // get course by it's id
        public async Task<ActionResult<ApiResponce>> GetCourseById(Guid id)
        {

            var course = await courseServices.GetCourseById(id);
            if (course.IsError) return NotFound(new ApiResponce { ErrorMassages =  course.FirstError.Description });                           
            return Ok(new ApiResponce { Result = mapper.Map<Course, CourseInformationDto>(course.Value) });
        }





        [HttpPut("EditCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]

        // edit course by who created before the admin accredit or reject the course
        public async Task<ActionResult<ApiResponce>> EditCourseBeforeAccredit(Guid id,[FromForm] CourseForEditDTO course)
        {
          
            
            var editCourse = await courseServices.EditOnCOurseBeforeAnAccredit(id , course);
            if (editCourse.IsError) return NotFound(new ApiResponce { ErrorMassages =  editCourse.FirstError.Description  });
            return Ok(new ApiResponce { Result = "The course is updated successfully" });
            
        }



        [HttpGet("GetallUndefinedCoursesToSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]

        // get all undefinded course created by a subAdmin depend on his id
        public async Task<ActionResult<ApiResponce>> GetALlUndefinedCoursesForSubAdmins(Guid subAdminId , [FromQuery] PaginationRequest paginationRequest)
        {          
                var getCourses = await courseServices.GetALlUndefinedCoursesForSubAdmins(subAdminId);
            if(getCourses.IsError) return NotFound(new ApiResponce { ErrorMassages =  getCourses.FirstError.Description  });
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInformationDto>>(getCourses.Value);
                return Ok(new ApiResponce { Result = (Pagination<CourseInformationDto>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });                          
        }

        [HttpGet("GetAllCoursesGivenByInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Instructor")]
        public async Task<ActionResult<ApiResponce>> GetAllCoursesByInstructorId(Guid Instructorid, [FromQuery] PaginationRequest paginationRequest)
        {

            var getCourses = await courseServices.GetAllCoursesByInstructor(Instructorid);
            if (getCourses.IsError) return NotFound(new ApiResponce {ErrorMassages = getCourses.FirstError.Description });
            
            return Ok(new ApiResponce { Result = (Pagination<Course>.CreateAsync(getCourses.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }


        [HttpGet("GetAllCustomCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Main-SubAdmin , Student")]
        // not try
        public async Task<ActionResult<ApiResponce>> GetAllCustomCoursesToMainSubAdmin([FromQuery] PaginationRequest paginationRequest)
        {

            var GetCustomCourses = await courseServices.GetAllCustomCourses();

            return Ok(new ApiResponce { Result= (Pagination<CustomCourseForRetriveDTO>.CreateAsync(GetCustomCourses, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
            }



        [HttpGet("GetCustomCoursesById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Main-SubAdmin , Student")]
        //not try
        public async Task<ActionResult<ApiResponce>> GetCustomCourseById(Guid id)
        {

            var GetCustomCourse = await courseServices.GetCustomCoursesById(id);
            if (GetCustomCourse.IsError) return NotFound(new ApiResponce { ErrorMassages = GetCustomCourse.FirstError.Description });
            return Ok(new ApiResponce { Result =GetCustomCourse.Value });

        }


        [HttpGet("GetAllEnrolledCoursesForAStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<ActionResult<ApiResponce>> GetEnrolledCourses(Guid studentid, [FromQuery] PaginationRequest paginationRequest)
        {

            var enrolledCourses = await courseServices.GetAllEnrolledCourses(studentid);
            return Ok( new ApiResponce { Result = 
                (Pagination<StudentCourse>.CreateAsync(enrolledCourses.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result}); 
            
        }


    }
    }

