using AutoMapper;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using courseProject.Services.Lectures;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models.DTO.UsersDTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly ILectureServices lectureServices;

        public LecturesController( ILectureServices lectureServices)
        {
            this.lectureServices = lectureServices;
        }


        [HttpGet("GetAllLectureRequest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        //yes [Authorize(Policy = "Main-SubAdmin ,Instructor , Student")]
        //not try
        public async Task<ActionResult<ApiResponce>> GetAllLecturesByInstructorId(Guid instructorId, [FromQuery] PaginationRequest paginationRequest)
        {

            var GetLectures = await lectureServices.GetAllLecturesByInstructorId(instructorId);
            if (GetLectures.IsError) return NotFound(new ApiResponce { ErrorMassages=GetLectures.FirstError.Description});
            return Ok(new ApiResponce { Result = (Pagination<LecturesForRetriveDTO>.CreateAsync(GetLectures.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
            

        }



        [HttpPost("BookALecture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //yes [Authorize(Policy = "Student")]
        //not try
        public async Task<ActionResult<ApiResponce>> BooKLectureByStudent(Guid studentId, DateTime date, string startTime, string endTime, [FromForm] BookALectureDTO bookALecture)
        {
            var lecture = await lectureServices.BookALecture(studentId , date, startTime, endTime, bookALecture);
            if (lecture.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce {ErrorMassages=lecture.FirstError.Description });
            else if (lecture.FirstError.Type == ErrorOr.ErrorType.Validation) return (new ApiResponce { ErrorMassages = lecture.FirstError.Description });
            return Ok(new ApiResponce { Result = "The Lecture is Created Successfully" });
        }



        [HttpPost("JoinToPublicLecture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //yes[Authorize(Policy = "Student")]
        //not try
        public async Task<ActionResult<ApiResponce>> JoinToAPublicLecture(Guid StudentId, Guid ConsultaionId)
        {
            var JoinedLecture = await lectureServices.JoinToPublicLecture(StudentId, ConsultaionId);
            if (JoinedLecture.IsError) return NotFound(new ApiResponce {ErrorMassages=JoinedLecture.FirstError.Description });
            return Ok(new ApiResponce { Result= "Joined successfully" });
        }


        [HttpPost("GetAllConsultations")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> GetAllConsultation(Guid studentId, [FromQuery] PaginationRequest paginationRequest)
        {
            var getLectures = await lectureServices.GetAllConsultations(studentId);
            if (getLectures.IsError) return NotFound(new ApiResponce { ErrorMassages=getLectures.FirstError.Description });         
            return Ok(new ApiResponce { Result= (Pagination<PublicLectureForRetriveDTO>.CreateAsync(getLectures.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }



        [HttpPost("GetConsultationById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> GetAConsultationById(Guid consultationId)
        {

            var getConsultation = await lectureServices.GetConsultationById(consultationId);
            if(getConsultation.IsError)  return NotFound(new ApiResponce {ErrorMassages=getConsultation.FirstError.Description });
            return Ok(new ApiResponce {Result=getConsultation.Value });
        }
    }
}
