using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using System.Net;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using courseProject.Repository.GenericRepository;
using System.Security.Claims;


namespace courseProject.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserContraller : ControllerBase
    {
       
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly projectDbContext dbContext;
        protected ApiResponce response;
        public UserContraller(   IUnitOfWork unitOfWork , IMapper mapper , projectDbContext dbContext)
        {
          
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.response = new();
        }



        [HttpPost("Login")]
        [AllowAnonymous]
        
        public async Task<IActionResult> Login( LoginRequestDTO loginRequestDTO)
        {
            
            var loginResponse = await unitOfWork.UserRepository.LoginAsync(loginRequestDTO);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add("Email or password is incorrect");
                return BadRequest(response);
            }
            response.IsSuccess = true;
            response.StatusCode=HttpStatusCode.OK;
            response.Result = loginResponse;
            return Ok(response);
        }



        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            if (model == null)
            {

                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                if (model.password != model.ConfirmPassword)
                {
                    response.ErrorMassages.Add("ConfirmPassword not equal password !");
                }
                else
                {
                    response.ErrorMassages.Add("Error while Registration !!");
                }
                return BadRequest(response);
            }
            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(model.email);

            if (!ifUserIsUniqe)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add("Email is already exists !!");
                return BadRequest(response);
            }
            //  var modelMapped = 


            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {
                   var mapp= await unitOfWork.UserRepository.RegisterAsync(model);
                    var success1 = await unitOfWork.StudentRepository.saveAsync();
                    
                   
                    if (model.role.ToLower() == "student")
                    { 
                        var idd = mapper.Map<User, Student>(mapp);
                        var modelMapped = mapper.Map<Student>(model);
                        modelMapped.StudentId = idd.StudentId;                    
                        await unitOfWork.StudentRepository.CreateStudentAccountAsync(modelMapped);
                        
                       
                    }
                    else if (model.role.ToLower() == "admin")
                    {
                        var adminMapper = mapper.Map<User, Admin>(mapp);
                      //  var modelMapped = mapper.Map<Admin>(model);
                      //  modelMapped.Id = model.UserId;
                        await unitOfWork.AdminRepository.CreateAdminAccountAsync(adminMapper);
                    }
                    var success2 = await unitOfWork.SubAdminRepository.saveAsync();

                    if (success1 > 0 && success2 > 0)
                    {
                        await transaction.CommitAsync();
                        response.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
                        response.IsSuccess = true;
                        response.Result = model;
                        return Ok(response);
                    }

                    return BadRequest(response);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();


                    response.StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest;
                    response.IsSuccess = false;

                    return BadRequest(response);
                }





            }
        }

        //[HttpPut("EditProfile")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult<ApiResponce>> EditProfileAsync( int id , ProfileDTO profile)
        //{
        //    if (id <= 0)
        //    {
        //            response.IsSuccess = false;
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
        //            return BadRequest(response);               
        //    }
        //    if (profile == null)
        //    {
        //        response.IsSuccess = false;
        //        response.StatusCode = HttpStatusCode.NotFound;
        //        response.ErrorMassages = new List<string>() { "the user is not found" };
        //        return NotFound(response);
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        response.IsSuccess = false;
        //        response.StatusCode = HttpStatusCode.BadRequest;
        //        return BadRequest(response);
        //    }

        //    var profileToUpdate = await dbContext.users.FirstOrDefaultAsync(x => x.UserId == id);
        //    if (profileToUpdate == null)
        //    {
        //        response.IsSuccess = false;
        //        response.StatusCode = HttpStatusCode.NotFound;
        //        response.ErrorMassages = new List<string>() { "the user is not found" };
        //        return NotFound(response);
        //    }
        //    using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //           // var userMapper = mapper.Map<ProfileDTO, User>(profile);
        //            profileToUpdate.userName = profile.FName;
        //            profileToUpdate.email = profile.email;
        //           // userMapper.UserId = id;
        //            await unitOfWork.UserRepository.updateSubAdminAsync(profileToUpdate);
        //            var success1 = await unitOfWork.UserRepository.saveAsync();
        //            var success2 = 0;
        //            if (profileToUpdate.role.ToLower() == "admin")
        //            {
        //                var adminMapper = mapper.Map<ProfileDTO, Admin>(profile);
        //                await unitOfWork.AdminRepository.updateSubAdminAsync(adminMapper);
        //                success2 = await unitOfWork.AdminRepository.saveAsync();
        //            }
        //            else if (profileToUpdate.role.ToLower() == "subadmin")
        //            {
        //                var subAdminMapper = mapper.Map<ProfileDTO, SubAdmin>(profile);
        //                await unitOfWork.SubAdminRepository.updateSubAdminAsync(subAdminMapper);
        //                success2 = await unitOfWork.SubAdminRepository.saveAsync();
        //            }
        //            else if (profileToUpdate.role.ToLower() == "instructor")
        //            {
        //                var instructorMapper = mapper.Map<ProfileDTO, Instructor>(profile);
        //                await unitOfWork.instructorRepositpry.updateSubAdminAsync(instructorMapper);
        //                success2 = await unitOfWork.instructorRepositpry.saveAsync();
        //            }
        //            else if (profileToUpdate.role.ToLower() == "student")
        //            {
        //                var studentMapper = mapper.Map<ProfileDTO, Student>(profile);
        //                await unitOfWork.StudentRepository.updateSubAdminAsync(studentMapper);
        //                success2 = await unitOfWork.StudentRepository.saveAsync();
        //            }

        //            if (success1 > 0 && success2 > 0)
        //            {
        //                await transaction.CommitAsync();
        //                response.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
        //                response.IsSuccess = true;
        //                response.Result = profile;
        //                return Ok(response);
        //            }

        //            return BadRequest(response);
        //        }
        //        catch (Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            response.StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest;
        //            response.IsSuccess = false;
        //            return BadRequest(response);
        //        }
        //    }
        //}



    }
}
