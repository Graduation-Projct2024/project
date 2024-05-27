using AutoMapper;
using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using courseProject.Services.Feedbacks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using courseProject.Core.IGenericRepository;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackServices feedbackServices;

        public FeedbackController(IFeedbackServices feedbackServices)
        {
            this.feedbackServices = feedbackServices;
        }


        [HttpPost("AddInstructorFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
       //not try
        public async Task<ActionResult<ApiResponce>> AddInstructorFeedback(Guid studentId, Guid InstructorId, FeedbackDTO Feedback)
        {
            var addedFeddback = await feedbackServices.AddInstructorFeedback(studentId, InstructorId, Feedback);
            if (addedFeddback.IsError) return NotFound(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            return Ok(new ApiResponce { Result = "The feedack is added successfully" });
        }



        [HttpPost("AddCourseFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<ActionResult<ApiResponce>> AddCourseFeedback(Guid studentId, Guid courseId, FeedbackDTO Feedback)
        {
            var addedFeddback = await feedbackServices.AddCourseFeedback(studentId, courseId, Feedback);
            if (addedFeddback.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            else if (addedFeddback.FirstError.Type == ErrorOr.ErrorType.Validation) return BadRequest(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            return Ok(new ApiResponce { Result = "The feedack is added successfully" });
        }


        [HttpPost("AddGeneralFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<ActionResult<ApiResponce>> AddGeneralFeedback(Guid studentId, FeedbackDTO Feedback)
        {
            var addedFeddback = await feedbackServices.AddGeneralFeedback(studentId, Feedback);
            if (addedFeddback.IsError) return NotFound(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            return Ok(new ApiResponce { Result = "The feedack is added successfully" });
        }



        [HttpGet("GetAllGeneralFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllGeneralFeedback([FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllGeneralFeedback();
            
            return Ok(new ApiResponce { Result = (Pagination<FeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }


        [HttpGet("GetAllInstructorFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllInstructorFeedback([FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllInstructorFeedback();

            return Ok(new ApiResponce { Result = (Pagination<FeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }


        [HttpGet("GetAllCourseFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllCourseFeedback([FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllCourseFeedback();

            return Ok(new ApiResponce { Result = (Pagination<FeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }


        [HttpGet("GetAllFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllFeedback([FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllFeedback();

            return Ok(new ApiResponce { Result = (Pagination<AllFeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }


        [HttpGet("GetFeedbackById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetFeedbackById(Guid id)
        {

            var getFeedback = await feedbackServices.GetFeedbackById(id);
            if (getFeedback.IsError) return NotFound(new ApiResponce { ErrorMassages = getFeedback.FirstError.Description });
            return Ok(new ApiResponce { Result = getFeedback.Value });
        }
    }
}
