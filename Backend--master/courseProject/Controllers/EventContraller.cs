﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using courseProject.Repository.GenericRepository;
using System.Net;

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
        public async Task<ActionResult< IReadOnlyList<Event>>> GetAllEventsAsync()
        {
            var events = await eventRepo.GetAllEventsAsync();
            if (events == null)
            {
                return NotFound();
            }
            var mapperEvent = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventDto>>(events);
            //var updatedEvents = mapperEvent.Select(events =>
            //{
            //    events.ImageUrl = $"http://localhost:5134/{events.ImageUrl}";
            //    return events;
            //}).ToList();
            return Ok(mapperEvent);
        }

        [HttpGet("GetAllUndefinedEvents")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IReadOnlyList<Event>>> GetAllEventsForAccreditAsync()
        {
            var events = await eventRepo.GetAllEventsForAccreditAsync();
            if(events == null)
            {
                return NotFound();
            }

            var mapperEvents = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventAccreditDto>>(events);
            
            return Ok(mapperEvents);
        }


        [HttpPost("EditEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
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
