using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
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

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseContraller : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly projectDbContext dbContext;
        private readonly IGenericRepository1<Course> courseRepo;
        private readonly IGenericRepository1<Request> requestRepo;
        private readonly IMapper mapper;
        protected ApiResponce responce;
        private Common.CommonClass CommonClass;
        //  private Request request;

        public CourseContraller(IUnitOfWork unitOfWork, projectDbContext dbContext, IGenericRepository1<Course> CourseRepo, IGenericRepository1<Request> RequestRepo, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
            courseRepo = CourseRepo;
            requestRepo = RequestRepo;
            this.mapper = mapper;
            responce = new ApiResponce();
            CommonClass = new Common.CommonClass();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        //get all accredits courses
        public async Task<ActionResult<ApiResponce>> GetAllCoursesAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var courses = await courseRepo.GetAllCoursesAsync();

            if (courses.Count() == 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages.Add("Not Has Any Accrefit Course Yet");
                return NotFound(responce);
            }
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInformationDto>>(courses);
            responce.IsSuccess = true;
            responce.StatusCode=HttpStatusCode.OK;
            responce.Result = (Pagination<CourseInformationDto>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            return Ok(responce);
        }

        [HttpGet("GetAllCoursesToStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Student")]
        public async Task<ActionResult<IReadOnlyList<Course>>> GetAllCourses(int studentId , [FromQuery] PaginationRequest paginationRequest)
        {
            try
            {


                var courses = await unitOfWork.StudentRepository.GetAllCoursesAsync(studentId);

                if (courses.Count() == 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add("Not Has Any Accrefit Course Yet");
                    return NotFound(responce);
                }
                var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInfoForStudentsDTO>>(courses);
                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                responce.Result = (Pagination<CourseInfoForStudentsDTO>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"{ex.Message}");
                return BadRequest(responce);
            }
        }

        [HttpGet("GetAllCoursesForAccredit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        //get all courses
        public async Task<ActionResult<IReadOnlyList<Course>>> GetAllCoursesForAccreditAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var courses = await courseRepo.GetAllCoursesForAccreditAsync();
            if (courses.Count() == 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages.Add("Not Has Any Course Yet");
                return NotFound(responce);
            }

            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseAccreditDTO>>(courses);

            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result = (Pagination<CourseAccreditDTO>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            
            return Ok(responce);
        }



        [HttpPost("CreateCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy ="SubAdmin")]
        public async Task<ActionResult<Course>> createCourse([FromForm] CourseForCreateDTO model , int? StudentId)
        {
            if (! model.GetType().GetProperties()
                .Select(x=>x.GetValue(model))
                .Any(Value =>Value !=null)  )
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { "the inputs is null" };
                return NotFound(responce);
            }
            if (!ModelState.IsValid)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(responce);
            }
            await unitOfWork.FileRepository.UploadFile1(model.image);
            
            var courseMapped = mapper.Map<Course>(model);
            var requestMapped = mapper.Map<Request>(model);
            var admin = await unitOfWork.UserRepository.GetUserByRoleAsync("admin");
            requestMapped.AdminId = admin.UserId;
            if(StudentId != null)
            {
                requestMapped.StudentId = StudentId;
            }
            courseMapped.ImageUrl = "Files\\"+ await unitOfWork.FileRepository.UploadFile1(model.image);

            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {
                    await unitOfWork.SubAdminRepository.CreateRequest(requestMapped);
                    var success1 = await unitOfWork.StudentRepository.saveAsync();

                    // courseMapped.Id = requestMapped.Id;

                    // var idd= mapper.Map<Request, Course>(requestMapped);
                    courseMapped.requestId = requestMapped.Id;
                    await unitOfWork.SubAdminRepository.CreateCourse(courseMapped);
                    var success2 = await unitOfWork.StudentRepository.saveAsync();

                    if (success1 > 0 && success2 > 0)
                    {
                        await transaction.CommitAsync();                        
                        responce.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
                        responce.IsSuccess = true;
                        responce.Result = model;
                        return Ok(responce);
                    }

                    return BadRequest(responce);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    responce.StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest;
                    responce.IsSuccess = false;
                    return BadRequest(responce);
                }
            }

        }


        [HttpPost("CreateEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy ="SubAdmin")]
        public async Task<ActionResult<Event>> createEvent([FromForm]EventForCreateDTO model)
        {
            await unitOfWork.FileRepository.UploadFile1(model.image);

            var EventMapped = mapper.Map<Event>(model);
            var requestMapped = mapper.Map<Request>(model);
            var admin = await unitOfWork.UserRepository.GetUserByRoleAsync("admin");
            requestMapped.AdminId = admin.UserId;
            EventMapped.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(model.image);
            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {
                    await unitOfWork.SubAdminRepository.CreateRequest(requestMapped);
                    var success1 = await unitOfWork.SubAdminRepository.saveAsync();

                    
                    var eventid = mapper.Map<Request, Event>(requestMapped);
                    EventMapped.requestId = eventid.Id;
                    await unitOfWork.SubAdminRepository.CreateEvent(EventMapped);
                    var success2 = await unitOfWork.SubAdminRepository.saveAsync();

                    await transaction.CommitAsync();

                    if (success1 > 0 && success2 > 0)
                    {
                        responce.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
                        responce.IsSuccess = true;
                        responce.Result = model;
                        return Ok(responce);
                    }

                    return BadRequest(responce);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();


                    responce.StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest;
                    responce.IsSuccess = false;

                    return BadRequest(responce);
                }
            }

        }


        [HttpPatch("accreditCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EditCourseStatus( int courseId , string Status )
        {            
            var entity = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (entity == null)
            {
                return NotFound();
            }            
            Expression<Func<Course, string>> path = x => x.status;
            var patchDocument = new JsonPatchDocument<Course>();
            patchDocument.Replace(path, Status);
            entity.status = Status;
            // jsonPatch.ApplyTo(entity, ModelState);
           // jsonPatch.Replace(path , "Accredit");
            await unitOfWork.SubAdminRepository.updateCourse(entity);
           await unitOfWork.SubAdminRepository.saveAsync();
            return Ok(entity);
        }
        

        [HttpPatch("accreditEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EditEventStatus(int eventId, string Status)
        {
            var entity = await unitOfWork.eventRepository.GetEventByIdAsync( eventId);

            if (entity == null)
            {
                return NotFound();
            }
            Expression<Func<Event, string>> path = x => x.status;
            var patchDocument = new JsonPatchDocument<Event>();
            patchDocument.Replace(path, Status);
            entity.status = Status;
            // jsonPatch.ApplyTo(entity, ModelState);
            // jsonPatch.Replace(path , "Accredit");
            await unitOfWork.SubAdminRepository.updateEvent(entity);
            await unitOfWork.SubAdminRepository.saveAsync();
            return Ok(entity);
        }


        [HttpGet("GetCourseById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetCourseById(int id)
        {
            if (id <= 0)
            {
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.IsSuccess = false;
                responce.ErrorMassages.Add($"The Course Id = {id} is less or equal 0 ");
                return BadRequest(responce);
            }
           
           var coursee = await  unitOfWork.CourseRepository.GetCourseByIdAsync(id);
            if (coursee == null )
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { $"The Course of Id = {id} does not exists" };
                responce.Result = null;
                return NotFound(responce);
            }
            var courseMapper = mapper.Map<Course, CourseInformationDto>(coursee);
            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result = courseMapper;
            return Ok(responce);
        }





        [HttpPost("EditCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //[Authorize(Policy = "subAdmin")]
        public async Task<ActionResult<ApiResponce>> EditCourseBeforeAccredit(int id,[FromForm] CourseForEditDTO course)
        {
            if (id <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(responce);
            }
            if (!ModelState.IsValid)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(responce);
            }
            var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(id);
            if (getCourse == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { $"the course with id = {id} is not found" };
                return NotFound(responce);
            }
            mapper.Map(course, getCourse);
            if (course.image != null)
            {
                getCourse.ImageUrl  = "Files\\" + await unitOfWork.FileRepository.UploadFile1(course.image);
            }
            await unitOfWork.SubAdminRepository.updateCourse(getCourse);
            if (await unitOfWork.CourseRepository.saveAsync() > 0)
            {
                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                responce.Result = getCourse;
                return Ok(responce);
            }
            responce.IsSuccess = false;
            responce.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(responce);
        }



        [HttpGet("GetallUndefinedCoursesToSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetALlUndefinedCoursesForSubAdmins(int subAdminId , [FromQuery] PaginationRequest paginationRequest)
        {
            try
            {

                var allUndefinedCourses = await unitOfWork.CourseRepository.GetAllUndefinedCoursesBySubAdminIdAsync(subAdminId);
                if (allUndefinedCourses.Count() == 0)
                {
                    responce.IsSuccess = true;
                    responce.StatusCode = HttpStatusCode.NoContent;
                    responce.ErrorMassages.Add("There is no Course whose status has not yet been determined ");
                    return responce;
                }

                var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInformationDto>>(allUndefinedCourses);
                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                responce.Result = (Pagination<CourseInformationDto>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
                return Ok(responce);

            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode=HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The error is :" + ex.Message);
                return BadRequest(responce);
            }
        }
    }
}
