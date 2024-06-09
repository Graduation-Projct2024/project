using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using System.Net;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using courseProject.Repository.GenericRepository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using Microsoft.Extensions.Caching.Memory;
using BCrypt.Net;
using courseProject.Services.Users;
using courseProject.Common;


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
   
        private readonly IMemoryCache memoryCache;
        private readonly IUserServices userServices;

        public UserContraller(   IUnitOfWork unitOfWork, IMapper mapper, projectDbContext dbContext, IMemoryCache memoryCache , IUserServices userServices)
        {

            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.response = new();
       
            this.memoryCache = memoryCache;
            this.userServices = userServices;
        }

        [HttpGet("GetUserIdFromToken")]
        [Authorize]
        public IActionResult GetUserIdFromToken()
        {
            var Id = HttpContext.User.FindFirstValue("UserId");          
            if (Id==null)
            {
                return Unauthorized("User ID not found in token");
            }
            return Ok(Id);
        }
        
       

        [HttpPost("Login")]
        [AllowAnonymous]
        
        public async Task<IActionResult> Login( LoginRequestDTO loginRequestDTO)
        {
            var login = await userServices.Login(loginRequestDTO);
            if (login.IsError) return Ok(new ApiResponce { ErrorMassages = login.FirstError.Description });
            return Ok(new ApiResponce { Result = login.Value });
        }



        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            var registeUser = await userServices.Register(model);
            if (registeUser.IsError) return Ok(new ApiResponce { ErrorMassages=registeUser.FirstError.Description});
            return Ok(new ApiResponce { Result = model});
        }
            
                    

        [HttpPost("addCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCode(string email, string code)
        {
            var codeAdded = await userServices.addCodeVerification(email, code);
            if(codeAdded.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages=codeAdded.FirstError.Description });
            else if (codeAdded.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = codeAdded.FirstError.Description });
            return Ok(new ApiResponce { Result="you are verified now"});
        }



        [HttpGet("reSendCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> reSendTheVerificationCode(string email)
        {
            var recendCode = await userServices.reSendTheVerificationCode(email);
            if (recendCode.FirstError.Type == ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages = recendCode.FirstError.Description });
            else if (recendCode.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = recendCode.FirstError.Description });
            return Ok(new ApiResponce { Result = "Another code has been sent" });
            
        }


        [HttpPut("EditProfile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> EditProfileAsync(Guid id,[FromForm] ProfileDTO profile)
        {
            var updatedProfile = await userServices.EditUserProfile(id, profile);
            if (updatedProfile.FirstError.Type == ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages = updatedProfile.FirstError.Description });
            else if (updatedProfile.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = updatedProfile.FirstError.Description });
            return Ok(new ApiResponce { Result = "Profile Updated Successfully." });
        }


        [HttpGet("GetProfileInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetUserInfo(Guid id)
        {
            var getUser = await userServices.GetProfileInfo(id);
            if (getUser.IsError) return NotFound(new ApiResponce {ErrorMassages=getUser.FirstError.Description });
            return Ok(new ApiResponce { Result=getUser.Value});
        }



        [HttpPatch("changePassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> changePassword (Guid UserId ,ChengePasswordDTO chengePasswordDTO)
        {
            var changedPass = await userServices.changePassword( UserId, chengePasswordDTO);
            if (changedPass.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages=changedPass.FirstError.Description});
            else if (changedPass.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = changedPass.FirstError.Description });
            return Ok(new ApiResponce { Result="Password Changed Successfully."});
        }


        [HttpPost("AddEmailForForgetPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> addEmail (EmailDTO emailDTO)
        {
            var user = await userServices.GetUserByEmail(emailDTO.email);
            if (user.IsError) return NotFound(new ApiResponce { ErrorMassages=user.FirstError.Description});
            return Ok(new ApiResponce { Result=emailDTO.email});
        }


        [HttpPatch("ForgetPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ForgetPassword(string email ,ForgetPasswordDTO forgetPassword)
        {
            var addPassword = await userServices.forgetPassword(email, forgetPassword);
            if (addPassword.IsError) return NotFound(new ApiResponce { ErrorMassages=addPassword.FirstError.Description});
            return Ok(new ApiResponce {Result= "The password has been modified successfully" });
        }
    }
}
