using AutoMapper;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using courseProject.Services.Requests;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models.DTO.StudentsDTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestServices requestServices;

        public RequestController(IRequestServices requestServices)
        {
            this.requestServices = requestServices;
        }

        [HttpGet("GetAllRequestToJoinCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //yes [Authorize(Policy = "MainSubAdmin")]
        //not try
        public async Task<ActionResult<ApiResponce>> GetAllRequestFromStudentsToJoinCourses([FromQuery] PaginationRequest paginationRequest)
        {
            
             var getRequests = await requestServices.GetAllRequestToJoinCourses();
            return Ok(new ApiResponce { Result = (Pagination<ViewTheRequestOfJoindCourseDTO>.CreateAsync(getRequests, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }


        [HttpPost("RequestToCreateCustomCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //yes [Authorize(Policy = "Student")]
        //not try
        public async Task<IActionResult> RequestToCreateACustomCourse(Guid studentid, [FromForm] StudentCustomCourseDTO studentCustomCourse)
        {
            var customCourse = await requestServices.RequestToCreateCustomCourse(studentid, studentCustomCourse);
            if (customCourse.IsError) return NotFound( new ApiResponce { ErrorMassages = customCourse.FirstError.Description });
            return Ok(new ApiResponce {Result="The Request For Create Custom Course is Sent Successfully" });
        }
    }
}
