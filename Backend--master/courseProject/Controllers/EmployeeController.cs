using AutoMapper;
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
        [Authorize(Policy = "Admin&subAdmin")]
        public async Task<ActionResult<IEnumerable<SubAdmin>>> GetAllEmployeeAsync()
        {
            var SubAdmins = await subAdminRepo.GetAllEmployeeAsync();
            var instructors = await instructorRepo.GetAllEmployeeAsync();
            if (SubAdmins == null && instructors == null)
            {
                return NotFound();
            }
            var mapperSubAdmin = mapper.Map<IEnumerable<SubAdmin>, IEnumerable<EmployeeDto>>(SubAdmins);
            var mapperInstructor = mapper.Map<IEnumerable<Instructor>, IEnumerable<EmployeeDto>>(instructors);

            var allEmployees = mapperSubAdmin.Concat(mapperInstructor);

            return Ok(allEmployees);
            


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



        [HttpPost("CreateSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SubAdmin>> CreateSubAdmin(EmployeeForCreate subadmin)
        {

            //var employeeMapped = mapper.Map<EmployeeForCreate, SubAdmin>(subadmin);
            //var userMapped = mapper.Map<EmployeeForCreate, User>(subadmin);
            //await  subAdminRepo.createSubAdminAccountAsync(employeeMapped);
            //await userRepo.createSubAdminAccountAsync(userMapped);
            //var success1 = await subAdminRepo.saveAsync();
            //var success2 = await userRepo.saveAsync();
            //if (success1 > 0 && success2 > 0)
            //{
            //    responce.StatusCode = HttpStatusCode.Created;
            //    responce.IsSuccess = true;
            //    responce.Result = subadmin;
            //    return Ok(responce);
            //}
            //responce.StatusCode = HttpStatusCode.BadRequest;
            //responce.IsSuccess = false;
            //return BadRequest(responce);
        


            //var employeeMapped = mapper.Map<EmployeeForCreate, SubAdmin>(subadmin);
            //var userMapped = mapper.Map<EmployeeForCreate, User>(subadmin);
            //await  subAdminRepo.createSubAdminAccountAsync(employeeMapped);
            //await userRepo.createSubAdminAccountAsync(userMapped);
            //var success1 = await subAdminRepo.saveAsync();
            //var success2 = await userRepo.saveAsync();
            //if (success1 > 0 && success2 > 0)
            //{
            //    responce.StatusCode = HttpStatusCode.Created;
            //    responce.IsSuccess = true;
            //    responce.Result = subadmin;
            //    return Ok(responce);
            //}
            //responce.StatusCode = HttpStatusCode.BadRequest;
            //responce.IsSuccess = false;
            //return BadRequest(responce);



            var subAdminMapped = mapper.Map<SubAdmin>(subadmin);
            var userMapped = mapper.Map<RegistrationRequestDTO>(subadmin);

            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {
                    await unitOfWork.UserRepository.RegisterAsync(userMapped);
                    var success1 = await unitOfWork.SubAdminRepository.saveAsync();

                    // var idd = mapper.Map<SubAdmin, RegistrationRequestDTO>(subAdminMapped);
                    //  subAdminMapped.email = idd.email;
                    await unitOfWork.SubAdminRepository.createSubAdminAccountAsync(subAdminMapped);
                    var success2 = await unitOfWork.SubAdminRepository.saveAsync();

                    await transaction.CommitAsync();

                    if (success1 > 0 && success2 > 0)
                    {
                        responce.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
                        responce.IsSuccess = true;
                        responce.Result = subadmin;
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
        public async Task<ActionResult<SubAdmin>> GetById(int id)
        {

            if (id == 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(responce);
            }

            var subadmin = await subAdminRepo.GetEmployeeById(id);
            var mappedemployee = mapper.Map<SubAdmin, EmployeeDto>(subadmin);
            if (subadmin == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { "Estate of {id} not exists" };
                return NotFound(responce);
            }
            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result = mappedemployee;
            return Ok(responce);
        }


        [HttpPost("CreateInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Instructor>> CreateInstructor(EmployeeForCreate instructor)
        {
            //var employeeMapped = mapper.Map<EmployeeForCreate, Instructor>(instructor);
            //var userMapped = mapper.Map<EmployeeForCreate, User>(instructor);
            //await instructorRepo.createSubAdminAccountAsync(employeeMapped);
            //await userRepo.createSubAdminAccountAsync(userMapped);
            //var success1 = await subAdminRepo.saveAsync();
            //var success2 = await userRepo.saveAsync();
            //if (success1 > 0 && success2 > 0)
            //{
            //    responce.StatusCode = HttpStatusCode.Created;
            //    responce.IsSuccess = true;
            //    responce.Result = instructor;
            //    return Ok(responce);
            //}
            //responce.StatusCode = HttpStatusCode.BadRequest;
            //responce.IsSuccess = false;
            //return BadRequest(responce);

            var instructorMapped = mapper.Map<Instructor>(instructor);
            var userMapped = mapper.Map<RegistrationRequestDTO>(instructor);

            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {
                    await unitOfWork.UserRepository.RegisterAsync(userMapped);
                    var success1 = await unitOfWork.SubAdminRepository.saveAsync();


                    await unitOfWork.instructorRepositpry.createInstructorAccountAsync(instructorMapped);
                    var success2 = await unitOfWork.SubAdminRepository.saveAsync();

                    await transaction.CommitAsync();

                    if (success1 > 0 && success2 > 0)
                    {
                        responce.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
                        responce.IsSuccess = true;
                        responce.Result = instructor;
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


        [HttpPut("UpdateSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> updateSubAdmin(int id, EmployeeDto subAdminModel)
            {

                if (id <= 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages = new List<string>() { "The Id is equal 0" };
                    return BadRequest(responce);
                }

                if (subAdminModel == null)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages = new List<string>() { "the subAdmin is not found" };
                    return NotFound(responce);
                }
                if (id != subAdminModel.Id || !ModelState.IsValid) {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responce);
                }

                var subAdminToUpdate = dbContext.subadmins.FirstOrDefault(x => x.Id == subAdminModel.Id);

                //  var subAdminToUpdate = await subAdminRepo.get


                if (subAdminToUpdate == null) {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages = new List<string>() { "the subAdmin is not found" };
                    return NotFound(responce);
                }

                var Subadminmapper = mapper.Map<EmployeeDto, SubAdmin>(subAdminModel);
                await subAdminRepo.updateSubAdminAsync(Subadminmapper);

                var success1 = await subAdminRepo.saveAsync();
                if (success1 > 0)
                {
                    responce.StatusCode = HttpStatusCode.OK;
                    responce.IsSuccess = true;
                    responce.Result = Subadminmapper;
                    return Ok(responce);
                }
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.IsSuccess = false;
                return BadRequest(responce);



            }



        }
    }
