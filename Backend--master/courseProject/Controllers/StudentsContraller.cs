using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using courseProject.Common;
using courseProject.core.Models;
using AutoMapper.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Services.Students;
using courseProject.Services.StudentCourses;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsContraller : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository1<Student> studentRepo;
        private readonly IMapper mapper;
        private readonly IStudentServices studentServices;
        private readonly IStudentCoursesServices studentCoursesServices;
        protected ApiResponce response;
        

        public StudentsContraller(IUnitOfWork unitOfWork, IGenericRepository1<Student> StudentRepo , IMapper mapper , IStudentServices studentServices
           )
        {
            this.unitOfWork = unitOfWork;
            studentRepo = StudentRepo;
            this.mapper = mapper;
            this.studentServices = studentServices;
            this.studentCoursesServices = studentCoursesServices;
            response =new ApiResponce();
            
        }


        [HttpGet("GetAllStudents")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        

        public async Task <IActionResult> GetAllStudentsAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var Students = await studentServices.GetAllStudents();
            return Ok(new ApiResponce { Result= 
                (Pagination<StudentsInformationDto>.CreateAsync(Students, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }


       



        [HttpGet("GetCourseParticipants")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , EnrolledInCourse")]
        
        public async Task<IActionResult> GetCourseParticipants(Guid Courseid, [FromQuery] PaginationRequest paginationRequest)
        {

            var StudentsParticipants = await studentServices.GetCourseParticipants(Courseid);
            if (StudentsParticipants.IsError) return NotFound(new ApiResponce { ErrorMassages= StudentsParticipants .FirstError.Description});
            return Ok(new ApiResponce { Result = 
                (Pagination<StudentsInformationDto>.CreateAsync(StudentsParticipants.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result});             
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

