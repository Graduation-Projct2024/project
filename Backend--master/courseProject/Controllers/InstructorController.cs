using AutoMapper;
using courseProject.Common;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using courseProject.Services.Instructors;
using courseProject.Core.IGenericRepository;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.EmployeesDTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IinstructorServices instructorServices;

        public InstructorController(IinstructorServices instructorServices)
        {
            this.instructorServices = instructorServices;
        }

        [HttpPost("AddInstructorOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Instructor")]
        public async Task<ActionResult<ApiResponce>> AddOfficeHours(Guid InstructorId, [FromForm] WorkingHourDTO _Working_Hours)
        {

           var addHours = await instructorServices.AddOfficeHours(InstructorId, _Working_Hours);
            if (addHours.FirstError.Type == ErrorOr.ErrorType.NotFound)
              return NotFound( new ApiResponce {ErrorMassages=addHours.FirstError.Description });

            else if (addHours.FirstError.Type == ErrorOr.ErrorType.Validation)
                return NotFound(new ApiResponce { ErrorMassages = addHours.FirstError.Description });
            return Ok(new ApiResponce {Result= "The Hours is added successfully" });

        }

        [HttpGet("GetInstructorOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> GetInstructorOfficeHourById(Guid Instructorid)
        {

           var getHours = await instructorServices.GetInstructorOfficeHours(Instructorid);
            if (getHours.IsError) return NotFound(new ApiResponce { ErrorMassages=getHours.FirstError.Description});
            return Ok(new ApiResponce {Result=getHours.Value });

        }


        [HttpGet("GetAllInstructorsList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> GetInstructorsForDropDownList()
        {
            return Ok(new ApiResponce { Result=await instructorServices.GetAllInstructorsList()});
        }


        [HttpGet("GetAllInstructorsOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> GetAllIinstructorsWithAllOfficeHours()
        {           
            return Ok(new ApiResponce {Result = await instructorServices.GetAllInstructorsOfficeHours() });
        }


        [HttpPost("GetListOfInstructorForLectures")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Student")]
        public async Task<ActionResult<ApiResponce>> GetAListOfInstrcutorsForBookALectures(Guid skillId, string startTime, string endTime, DateTime date)
        {
            var getInstructors = await instructorServices.GetListOfInstructorForLectures(skillId, startTime, endTime, date);
            if (getInstructors.IsError) return Ok(new ApiResponce { ErrorMassages = getInstructors.FirstError.Description });
            return Ok(new ApiResponce { Result = getInstructors });
        }


    }
}
