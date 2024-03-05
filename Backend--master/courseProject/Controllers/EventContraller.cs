using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventContraller : ControllerBase
    {
        private readonly IGenericRepository1<Event> eventRepo;
        private readonly IMapper mapper;

        public EventContraller(IGenericRepository1<Event> EventRepo , IMapper mapper)
        {
            eventRepo = EventRepo;
            this.mapper = mapper;
        }

        [HttpGet]
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
            return Ok(mapperEvent);
        }

        [HttpPost]
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

    }
}
