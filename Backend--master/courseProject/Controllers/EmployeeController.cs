using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using courseProject.Repository.GenericRepository;
using System.Net;
using courseProject.core.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Services.Employees;
using courseProject.Services.Instructors;
using courseProject.Services.SubAdmins;
using courseProject.Services.Users;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly projectDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEmployeeServices employeeServices;
        private readonly ISubAdminServices subAdminServices;
        private readonly IinstructorServices instructorServices;




        public EmployeeController(projectDbContext dbContext,  IMapper mapper , IEmployeeServices employeeServices  ,
             ISubAdminServices subAdminServices , IinstructorServices instructorServices)
            
        {
            this.dbContext = dbContext;          
            this.mapper = mapper;
            this.employeeServices = employeeServices;
            this.subAdminServices = subAdminServices;
            this.instructorServices = instructorServices;

        }

        [HttpGet("GetAllEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]      
        public async Task<IActionResult> GetAllEmployeeAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var allEmployees = await employeeServices.getAllEmployees();         
            return Ok(new ApiResponce { Result = (Pagination<EmployeeDto>.CreateAsync(allEmployees, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });               
        }


        //[HttpGet("GetAllEmployeeForContact")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        
        //public async Task<ActionResult<ApiResponce>> GetAllEmployeeForContactAsync([FromQuery] PaginationRequest paginationRequest)
        //{
        //    var subAdmins = await subAdminServices.GetAllSubAdmins();
        //    var instructors = await instructorServices.GetAllInstructors();

           
        //    var mapperSubAdmin = mapper.Map<IReadOnlyList<SubAdmin>, IReadOnlyList<ContactDto>>(subAdmins);
        //    var mapperInstructor = mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<ContactDto>>(instructors);
        //    var allEmployees = (mapperSubAdmin.Concat(mapperInstructor)).ToList();
         
        //    return Ok(new ApiResponce { Result = (Pagination<ContactDto>.CreateAsync(allEmployees, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        //}



        [HttpPost("CreateEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> CreateEmployee([FromForm]EmployeeForCreate model)
        {
          
            var createEmployee = await employeeServices.CreateEmployee(model);
            if (createEmployee.FirstError.Type == ErrorOr.ErrorType.Failure) return BadRequest(new ApiResponce { 
                ErrorMassages= createEmployee.FirstError.Description } );

            else if (createEmployee.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok( new ApiResponce { 
                ErrorMassages =  createEmployee.FirstError.Description  });

             return Ok(new ApiResponce { Result="The employee is create successfully" } );
        }
    



        [HttpGet("GetEmployeeById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {

            var getEmployee = await employeeServices.GetEmployeeById(id);
            if(getEmployee.IsError) return NotFound(new ApiResponce { ErrorMassages= getEmployee.FirstError.Description });
            return Ok(new ApiResponce { Result = getEmployee.Value });
        }


        [HttpPut("UpdateEmployeeFromAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> updateEmployee(Guid id,[FromForm] EmployeeForUpdateDTO EmpolyeeModel)
        {
            var updateEmployee = await employeeServices.UpdateEmployeeFromAdmin(id, EmpolyeeModel);
            if(updateEmployee.FirstError.Type==ErrorOr.ErrorType.NotFound) 
                return NotFound(new ApiResponce { ErrorMassages = updateEmployee.FirstError.Description });
            else if(updateEmployee.FirstError.Type == ErrorOr.ErrorType.Failure) return BadRequest(new ApiResponce { ErrorMassages = updateEmployee.FirstError.Description });

            return Ok(new ApiResponce { Result ="The employee is updated successfully"});
        }


       


        [HttpPatch("EditroleBetweenSubAdmin&MainSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> EditRole (Guid userId ,string role)
        {
            var editRole = await employeeServices.EditRole(userId, role);
            if (editRole.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce {ErrorMassages=editRole.FirstError.Description });
            else if (editRole.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = editRole.FirstError.Description });

            return Ok(new ApiResponce { Result = "The role is edited successfully" });
        }

    }
}
