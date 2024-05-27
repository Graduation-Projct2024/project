using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EventsDTO;
using courseProject.Repository.GenericRepository;
using courseProject.ServiceErrors;
using ErrorOr;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using System.Net;

namespace courseProject.Services.Events
{
    public class EventServices : IEventServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EventServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

       

        public async Task<ErrorOr<Created>> CreateEvent(Event _event, Request request)
        {
            
            var SubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(_event.SubAdminId, "subadmin");
            if (SubAdminFound == null) return ErrorSubAdmin.NotFound;
           
            if (_event.image != null)
            {
                _event.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(_event.image);
            }
            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {

                await unitOfWork.SubAdminRepository.CreateRequest(request);
                var success1 = await unitOfWork.StudentRepository.saveAsync();
                _event.requestId = request.Id;
                await unitOfWork.SubAdminRepository.CreateEvent(_event);
                var success2 = await unitOfWork.StudentRepository.saveAsync();

                if (success1 > 0 && success2 > 0)
                {
                    await transaction.CommitAsync();
                    return Result.Created;
                }

                return ErrorEvent.hasError;

            }
        }

        public async Task<ErrorOr<Updated>> accreditEvent(Guid eventId, string Status)
        {
            var getEvent = await unitOfWork.eventRepository.GetEventByIdAsync(eventId);
            if (getEvent == null) return ErrorEvent.NotFound;

            Expression<Func<Course, string>> path = x => x.status;
            var patchDocument = new JsonPatchDocument<Course>();
            patchDocument.Replace(path, Status);
            getEvent.status = Status;

            await unitOfWork.SubAdminRepository.updateEvent(getEvent);
            await unitOfWork.SubAdminRepository.saveAsync();
            return Result.Updated;
        }

        public async Task<IReadOnlyList<EventDto>> GetAllAccreditEvents()
        {
            var events = await unitOfWork.eventRepository.GetAllEventsAsync();
            
            events = events.OrderByDescending(x => x.dateOfAdded).ToList();
            var mapperEvent = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventDto>>(events);
            return mapperEvent;
        }

        public async Task<IReadOnlyList<EventAccreditDto>> GetAllEventsToAccreditByAdmin()
        {
            var events = await unitOfWork.eventRepository.GetAllEventsForAccreditAsync();
            var mapperEvents = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventAccreditDto>>(events);
            return mapperEvents;
        }

        public async Task<IReadOnlyList<EventAccreditDto>> GetAllUndefinedEvents(Guid subAdminId)
        {
            var events = await unitOfWork.eventRepository.GetAllUndefindEventBySubAdminIdAsync(subAdminId);
            var mapperEvents = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventAccreditDto>>(events);
            return mapperEvents;
        }

        public async Task<ErrorOr<Updated>> EditEvent(Guid eventId, EventForEditDTO eventForEditDTO)
        {

            var getEvent = await unitOfWork.eventRepository.GetEventByIdAsync(eventId);
            if (getEvent == null) return ErrorEvent.NotFound;
            
            mapper.Map(eventForEditDTO, getEvent);
            if (eventForEditDTO.image != null)
            {
                getEvent.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(eventForEditDTO.image);
            }
            await unitOfWork.SubAdminRepository.updateEvent(getEvent);
            await unitOfWork.CourseRepository.saveAsync() ;
            return Result.Updated;
        

    }
}
}
