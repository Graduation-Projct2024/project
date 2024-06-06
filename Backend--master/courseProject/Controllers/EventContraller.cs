using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.EventsDTO;
using courseProject.Services.Courses;
using courseProject.Services.Events;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventContraller : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository1<Event> eventRepo;
        private readonly IMapper mapper;
        private readonly IEventServices eventServices;
        private readonly ApiResponce responce;

        public EventContraller(  IUnitOfWork unitOfWork,IGenericRepository1<Event> EventRepo , IMapper mapper , IEventServices eventServices)
        {
            this.unitOfWork = unitOfWork;
            eventRepo = EventRepo;
            this.mapper = mapper;
            this.eventServices = eventServices;
            responce = new ApiResponce();
        }

        [HttpGet("GetAllAccreditEvents")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //not try
        public async Task<ActionResult< ApiResponce>> GetAllAccreditEventsAsync(string? dateStatus ,[FromQuery] PaginationRequest paginationRequest)
        {
            var events = await eventServices.GetAllAccreditEvents(dateStatus);
            return Ok(new ApiResponce { Result = (Pagination<EventDto>.CreateAsync(events, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }

        [HttpGet("GetAllEventsToAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        //no try
        public async Task<ActionResult<ApiResponce>> GetAllEventsToAccreditAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var events = await eventServices.GetAllEventsToAccreditByAdmin();
            return Ok( new ApiResponce { Result = (Pagination<EventAccreditDto>.CreateAsync(events, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
            
        }



        [HttpGet("GetAllUndefinedEventsToSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Main-SubAdmin , SubAdmin")]
        //not try
        public async Task<ActionResult<ApiResponce>> GetAllEvents(Guid subAdminId, [FromQuery] PaginationRequest paginationRequest)
        {
            var events = await eventServices.GetAllUndefinedEvents(subAdminId);
            return Ok(new ApiResponce { Result = (Pagination<EventAccreditDto>.CreateAsync(events, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }


        [HttpPost("CreateEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]
        public async Task<ActionResult<ApiResponce>> createEvent([FromForm] EventForCreateDTO model)
        {

            if (!model.GetType().GetProperties()
                .Select(x => x.GetValue(model))
                .Any(Value => Value != null))
            {

                responce.ErrorMassages =  "the inputs is null" ;
                return NotFound(responce);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(responce);
            }

            var EventMapped = mapper.Map<Event>(model);
            var requestMapped = mapper.Map<Request>(model);

            var createCourse = await eventServices.CreateEvent(EventMapped, requestMapped);
            if (createCourse.IsError) return NotFound(new ApiResponce { ErrorMassages =  createCourse.FirstError.Description  });
            return new ApiResponce { Result = "The Event Is Created Successfully" };
            
            }

        [HttpPatch("accreditEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResponce>> EditEventStatus(Guid eventId, EventStatusDTO eventStatus)
        {
            var updateStatus = await eventServices.accreditEvent(eventId ,eventStatus.Status);
            if (updateStatus.IsError) return NotFound(new ApiResponce
            {
                ErrorMassages =  updateStatus.FirstError.Description 
            });
            return Ok(new ApiResponce
            {
                Result = $"The Event is {eventStatus.Status}"
            });
        }


        [HttpPut("EditEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin, Main-SubAdmin , SubAdmin")]
       //nit try
        public async Task<ActionResult<ApiResponce>> EditEvent(Guid id, [FromForm] EventForEditDTO eventForEditDTO)
        {

            
            var editEvent = await eventServices.EditEvent(id, eventForEditDTO);
            if (editEvent.IsError) return NotFound(new ApiResponce { ErrorMassages=editEvent.FirstError.Description});
            return Ok(new ApiResponce { Result="The event is updated successfully"});                                                               
        }

    }
}
