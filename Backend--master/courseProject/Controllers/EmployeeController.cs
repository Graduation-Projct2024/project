﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using courseProject.Repository.Data;
using courseProject.Repository.GenericRepository;
using System.Net;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly projectDbContext dbContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository1<SubAdmin> subAdminRepo;
        private readonly IGenericRepository1<Instructor> instructorRepo;
        private readonly IGenericRepository1<User> userRepo;
        private readonly IMapper mapper;
        protected ApiResponce responce;


        public EmployeeController(projectDbContext dbContext, IUnitOfWork unitOfWork, IGenericRepository1<SubAdmin> SubAdminRepo, IGenericRepository1<Instructor> InstructorRepo, IGenericRepository1<User> userRepo, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.unitOfWork = unitOfWork;
            subAdminRepo = SubAdminRepo;
            instructorRepo = InstructorRepo;
            this.userRepo = userRepo;
            this.mapper = mapper;
            responce = new ApiResponce();
        }

        [HttpGet("GetAllEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // [Authorize(Policy = "Admin")]
       // [Authorize(Policy = "Admin&subAdmin")]
        public async Task<ActionResult<IEnumerable<SubAdmin>>> GetAllEmployeeAsync()
        {
            var SubAdmins = await subAdminRepo.GetAllEmployeeAsync();
            
            var instructors = await instructorRepo.GetAllEmployeeAsync();
            if (SubAdmins == null && instructors == null)
            {
                return NotFound();
            }
            var mapperSubAdmin = mapper.Map<IEnumerable<SubAdmin>, IEnumerable<EmployeeDto>>(SubAdmins);
            foreach (var SubAdmin in mapperSubAdmin)
            {
                SubAdmin.type = "SubAdmin";
            }
            var mapperInstructor = mapper.Map<IEnumerable<Instructor>, IEnumerable<EmployeeDto>>(instructors);
            foreach (var instructor in mapperInstructor)
            {
                instructor.type = "Instructor";
            }
            var allEmployees = mapperSubAdmin.Concat(mapperInstructor);
           
            return Ok(allEmployees.OrderBy(x=>x.Id));
            


        }


        [HttpGet("GetAllEmployeeForContact")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IReadOnlyList<SubAdmin>>> GetAllEmployeeForContactAsync()
        {
            var subAdmins = await subAdminRepo.GetAllEmployeeForContactAsync();
            var instructors = await instructorRepo.GetAllEmployeeForContactAsync();

            if (subAdmins == null && instructors == null)
            {
                return NotFound();
            }
            var mapperSubAdmin = mapper.Map<IReadOnlyList<SubAdmin>, IReadOnlyList<ContactDto>>(subAdmins);
            var mapperInstructor = mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<ContactDto>>(instructors);
            var allEmployees = mapperSubAdmin.Concat(mapperInstructor);

            return Ok(allEmployees);
        }



        [HttpPost("CreateEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateEmployee([FromForm]EmployeeForCreate model)
        {
            if (model == null)
            {

                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.IsSuccess = false;
                
                return BadRequest(responce);
            }
            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(model.email);

            if (!ifUserIsUniqe)
            {
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.IsSuccess = false;
                responce.ErrorMassages.Add("Email is already exists !!");
                return BadRequest(responce);
            }         
            var userMapped = mapper.Map<RegistrationRequestDTO>(model);

            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {
                    var Usermapp=await unitOfWork.UserRepository.RegisterAsync(userMapped);
                    var success1 = await unitOfWork.SubAdminRepository.saveAsync();
                    if (model.role.ToLower() == "subadmin")
                    {
                        var modelMapped = mapper.Map<SubAdmin>(model);
                        var userMap = mapper.Map<User, SubAdmin>(Usermapp);
                        modelMapped.SubAdminId = userMap.SubAdminId;
                        await unitOfWork.SubAdminRepository.createSubAdminAccountAsync(modelMapped);
                    } 
                    else 
                    {
                        var modelMapped = mapper.Map<Instructor>(model);
                        var userMap = mapper.Map<User, Instructor>(Usermapp);
                        modelMapped.InstructorId = userMap.InstructorId;
                        await unitOfWork.instructorRepositpry.createInstructorAccountAsync(modelMapped);
                    }
                    var success2 = await unitOfWork.SubAdminRepository.saveAsync();

                    

                    if (success1 > 0 && success2 > 0)
                    {
                        await transaction.CommitAsync();
                        responce.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
                        responce.IsSuccess = true;
                        responce.Result = model;
                        return Ok(responce);
                    }

                    return BadRequest(responce);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();


                    responce.StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest;
                    responce.IsSuccess = false;

                    return BadRequest(responce);
                }

            }
        }



        [HttpGet("GetEmployeeById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetById(int id)
        {

            if (id <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(responce);
            }
            var subAdminToGet = await dbContext.subadmins.FirstOrDefaultAsync(x => x.SubAdminId == id);
            var instructorToGet = await dbContext.instructors.FirstOrDefaultAsync(x => x.InstructorId == id);
            var UserToGet = await dbContext.users.FirstOrDefaultAsync(x => x.UserId == id);
            if (UserToGet == null )
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { $"Employee of Id = {id} does not exists" };
                responce.Result = null;
                return NotFound(responce);
            }
            SubAdmin Subadmin = null;
            Instructor Instructor = null;
            if (subAdminToGet == null && UserToGet.role.ToLower() == "subadmin")
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { "the subAdmin is not found" };
                return NotFound(responce);
            }
            else if (subAdminToGet != null && UserToGet.role.ToLower() == "subadmin")
            {
                 Subadmin = await unitOfWork.SubAdminRepository.GetEmployeeById(id);
                var mappedEmployee = mapper.Map<SubAdmin, EmployeeDto>(Subadmin);
                mappedEmployee.type = "SubAdmin";
                responce.Result= mappedEmployee;
            }
            if (instructorToGet == null && UserToGet.role.ToLower() == "instructor")
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { "the instructor is not found" };
                return NotFound(responce);
            }
            else if (instructorToGet != null && UserToGet.role.ToLower() == "instructor")
            {
                Instructor = await unitOfWork.instructorRepositpry.GetEmployeeById(id);
                var mappedEmployee = mapper.Map<Instructor, EmployeeDto>(Instructor);
                mappedEmployee.type = "Instructor";
                responce.Result = mappedEmployee;
            }           
            if (Subadmin == null && Instructor ==null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { $"Employee of Id = {id} does not exists" };
                responce.Result = null;
                return NotFound(responce);
            }
            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            return Ok(responce);
        }



      


       




        }
}
