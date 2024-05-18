using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using courseProject.Repository.GenericRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventContraller : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository1<Event> eventRepo;
        private readonly IMapper mapper;
        private readonly ApiResponce responce;

        public EventContraller(  IUnitOfWork unitOfWork,IGenericRepository1<Event> EventRepo , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            eventRepo = EventRepo;
            this.mapper = mapper;
            responce = new ApiResponce();
        }

        [HttpGet("GetAllAccreditEvents")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult< ApiResponce>> GetAllEventsAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var events = await eventRepo.GetAllEventsAsync();
            if (events == null)
            {
                responce.StatusCode = HttpStatusCode.NoContent;
                responce.ErrorMassages.Add("There is no accredit events yet");
                return Ok(responce);
                
            }
            var mapperEvent = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventDto>>(events);
            //var updatedEvents = mapperEvent.Select(events =>
            //{
            //    events.ImageUrl = $"http://localhost:5134/{events.ImageUrl}";
            //    return events;
            //}).ToList();
            responce.IsSuccess = true;
            responce.Result = (Pagination<EventDto>.CreateAsync(mapperEvent, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            return Ok(responce);
        }

        [HttpGet("GetAllUndefinedEvents")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllEventsForAccreditAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var events = await eventRepo.GetAllEventsForAccreditAsync();
            if(events == null)
            {
                responce.StatusCode = HttpStatusCode.NoContent;
                responce.ErrorMassages.Add("There is no events yet");
                return Ok(responce); 
            }

            var mapperEvents = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventAccreditDto>>(events);
            responce.IsSuccess = true;
            responce.Result = (Pagination<EventAccreditDto>.CreateAsync(mapperEvents, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            return Ok(responce);
        }


        [HttpPost("EditEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]
        public async Task<ActionResult<ApiResponce>> EditEvent(int id, [FromForm] EventForEditDTO eventForEditDTO)
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
            var getEvent = await unitOfWork.eventRepository.GetEventByIdAsync(id);
            if (getEvent == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { $"the Event with id = {id} is not found" };
                return NotFound(responce);
            }
            mapper.Map(eventForEditDTO, getEvent);
            if (eventForEditDTO.image != null)
            {
                getEvent.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(eventForEditDTO.image);
            }
            await unitOfWork.SubAdminRepository.updateEvent(getEvent);
            if (await unitOfWork.CourseRepository.saveAsync() > 0)
            {
                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                responce.Result = getEvent;
                return Ok(responce);
            }
            responce.IsSuccess = false;
            responce.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(responce);
        }

    }
}
