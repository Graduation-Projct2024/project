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


namespace courseProject.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserContraller : ControllerBase
    {
       
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        protected ApiResponce response;
        public UserContraller(   IUnitOfWork unitOfWork , IMapper mapper)
        {
          
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
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
                    await unitOfWork.UserRepository.RegisterAsync(model);
                    var success1 = await unitOfWork.StudentRepository.saveAsync();
                    if (model.role.ToLower() == "student")
                    {
                        var modelMapped = mapper.Map<Student>(model);
                        await unitOfWork.StudentRepository.CreateStudentAccountAsync(modelMapped);
                    }
                    else if (model.role.ToLower() == "admin")
                    {
                        var modelMapped = mapper.Map<Admin>(model);
                        await unitOfWork.AdminRepository.CreateAdminAccountAsync(modelMapped);
                    }
                    var success2 = await unitOfWork.StudentRepository.saveAsync();



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


       

    }
}
