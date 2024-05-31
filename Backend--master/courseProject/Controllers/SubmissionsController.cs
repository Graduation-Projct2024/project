using AutoMapper;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.core.Models;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using courseProject.Services.Submissions;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models.DTO.MaterialsDTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionServices submissionServices;

        public SubmissionsController( ISubmissionServices submissionServices)
        {
            this.submissionServices = submissionServices;
        }

        [HttpGet("GetAllSubmissionForTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        // not try
        public async Task<ActionResult<ApiResponce>> GetAllSubmissionUsingTaskId(Guid taskId, [FromQuery] PaginationRequest paginationRequest)
        {
            var getSubmissions = await submissionServices.GetAllSubmissionForTask(taskId);
            if (getSubmissions.IsError) return NotFound(new ApiResponce { ErrorMassages=getSubmissions.FirstError.Description});
            return Ok(new ApiResponce { Result = (Pagination<StudentSubmissionDTO>.CreateAsync(getSubmissions.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }


        [HttpPost("AddTaskSubmission")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "MaterialInEnrolledCourseForStudent")]
        public async Task<ActionResult<ApiResponce>> AddTaskByStudent(Guid Studentid, Guid Id, [FromForm] SubmissionsDTO submissions)
        {
            var addedTask = await submissionServices.AddTaskSubmission(Studentid, Id, submissions);
            if (addedTask.IsError) return NotFound(new ApiResponce { ErrorMassages = addedTask.FirstError.Description });
            return Ok(new ApiResponce {Result="The Task Is Added Successfully" });
        }
    }
}
