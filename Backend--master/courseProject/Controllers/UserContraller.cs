using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using System.Net;

namespace courseProject.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserContraller : ControllerBase
    {
       
        private readonly IUnitOfWork unitOfWork;
        protected ApiResponce response;
        public UserContraller(   IUnitOfWork unitOfWork)
        {
          
            this.unitOfWork = unitOfWork;
            this.response = new();
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
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

        public async Task<IActionResult> Register([FromForm] RegistrationRequestDTO registrationRequestDTO)
        {
            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(registrationRequestDTO.email);

            if (!ifUserIsUniqe)
            {
                response.StatusCode=HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages.Add("Email is already exists !!");
                return BadRequest(response);
            }
            var user =await unitOfWork.UserRepository.RegisterAsync(registrationRequestDTO);
            if(user == null)
            {
                
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                if(registrationRequestDTO.password != registrationRequestDTO.ConfirmPassword)
                {
                    response.ErrorMassages.Add("ConfirmPassword not equal password !");
                }
                else
                {
                    response.ErrorMassages.Add("Error while Registration !!");
                }
                return BadRequest(response);
            }
            response.StatusCode=HttpStatusCode.OK;
            response.IsSuccess=true;
            
            return Ok(response);


        }


    }
}
