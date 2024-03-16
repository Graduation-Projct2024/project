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

        //  private Request request;

        public CourseContraller(IUnitOfWork unitOfWork, projectDbContext dbContext, IGenericRepository1<Course> CourseRepo, IGenericRepository1<Request> RequestRepo, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
            courseRepo = CourseRepo;
            requestRepo = RequestRepo;
            this.mapper = mapper;
            responce = new ApiResponce();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IReadOnlyList<Course>>> GetAllCoursesAsync()
        {
            var courses = await courseRepo.GetAllCoursesAsync();

            if (courses == null)
            {
                return NotFound();
            }
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInformationDto>>(courses);
            var updatedCourses = mapperCourse.Select(course =>
            {
                course.ImageUrl = $"http://localhost:5134/{course.ImageUrl}";
                return course;
            }).ToList();
            return Ok(mapperCourse);
        }


        [HttpGet("GetAllCoursesForAccredit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IReadOnlyList<Course>>> GetAllCoursesForAccreditAsync()
        {
            var courses = await courseRepo.GetAllCoursesForAccreditAsync();
            if (courses == null)
            {
                return NotFound();
            }

            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseAccreditDTO>>(courses);
            var updatedCourses = mapperCourse.Select(course =>
            {
                course.ImageUrl = $"http://localhost:5134/{course.ImageUrl}";
                return course;
            }).ToList();
            
            return Ok(mapperCourse);
        }


        //[HttpPost("CreateCourse")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult<ApiResponce>> createCourse( CourseForCreateDTO model  )
        //{

        //   //request.name = model.name;
        //    //request.satus = "off";
        //    //request.date= DateTime.Now ;
        //    //request.adminId = 1;
        //    var courseMapped = mapper.Map<CourseForCreateDTO, Course>(model);
        //    var requestMapped = mapper.Map<CourseForCreateDTO, Request>(model);
        //    // var id = requestMapped.Id;
        //   // model.Id = id;

        //     unitOfWork.SubAdminRepository.CreateRequest(requestMapped);
        //    var success2 = await unitOfWork.SubAdminRepository.saveAsync();

        //   model.requestId = requestMapped.Id;
        //     unitOfWork.SubAdminRepository.CreateCourse(courseMapped);
        //    var success1 = await unitOfWork.SubAdminRepository.saveAsync();

        //    if (success1 > 0 && success2>0)
        //    {
        //        responce.StatusCode = HttpStatusCode.Created;
        //        responce.IsSuccess = true;
        //        responce.Result = model;
        //        return Ok(responce);
        //    }
        //    responce.StatusCode = HttpStatusCode.BadRequest;
        //    responce.IsSuccess = false;
        //    return BadRequest(responce);
        //}




        [HttpPost("CreateCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Course>> createCourse([FromForm] CourseForCreateDTO model , int? StudentId)
        {
            if (model == null)
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
        public async Task<ActionResult<Event>> createEvent([FromForm]EventForCreateDTO model)
        {
            await unitOfWork.FileRepository.UploadFile1(model.image);

            var EventMapped = mapper.Map<Event>(model);
            var requestMapped = mapper.Map<Request>(model);
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
      //  JsonPatchDocument<Course> jsonPatch;

        [HttpPatch("accreditCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EditCourseStatus( int courseId , string Status )
        {            
            var entity = await dbContext.courses.FirstOrDefaultAsync(x => x.Id == courseId);
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
            var entity = await dbContext.events.FirstOrDefaultAsync(x => x.Id == eventId);

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

        //[HttpPost("EditCourse")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult<ApiResponce>> EditCourse(int id , CourseForEditDTO course)
        //{
        //    if (id <= 0)
        //    {
        //        responce.IsSuccess = false;
        //        responce.StatusCode = HttpStatusCode.BadRequest;
        //        responce.ErrorMassages = new List<string>() { "The Id is equal 0" };
        //        return BadRequest(responce);
        //    }

        //    if (course == null)
        //    {
        //        responce.IsSuccess = false;
        //        responce.StatusCode = HttpStatusCode.NotFound;
        //        responce.ErrorMassages = new List<string>() { "No new data to updated" };
        //        return BadRequest(responce);
        //    }
        //    if (id != course.Id || !ModelState.IsValid)
        //    {
        //        responce.IsSuccess = false;
        //        responce.StatusCode = HttpStatusCode.BadRequest;
        //        return BadRequest(responce);
        //    }

        //    var getCourse= await dbContext.courses.FirstOrDefaultAsync(x=>x.Id == course.Id);
        //    if (getCourse == null)
        //    {
        //        responce.IsSuccess = false;
        //        responce.StatusCode = HttpStatusCode.NotFound;
        //        responce.ErrorMassages = new List<string>() { "the course is not found" };
        //        return NotFound(responce);
        //    }
        //}

    }
}
