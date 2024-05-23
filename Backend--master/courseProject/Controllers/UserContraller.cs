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
        private Common.CommonClass CommonClass;
        private readonly IMemoryCache memoryCache;
        public UserContraller(   IUnitOfWork unitOfWork, IMapper mapper, projectDbContext dbContext, IMemoryCache memoryCache)
        {

            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.response = new();
            CommonClass = new Common.CommonClass();
            this.memoryCache = memoryCache;
        }

        [HttpGet("GetUserIdFromToken")]
        [Authorize]
        public IActionResult GetUserIdFromToken()
        {
            var Id = Convert.ToInt32( HttpContext.User.FindFirstValue("UserId"));
            //var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            //  var role = (HttpContext.User.FindFirstValue("role"));
            if (Id==null)
            {
                return Unauthorized("User ID not found in token");
            }
            return Ok(Id );
        }
        
       

        [HttpPost("Login")]
        [AllowAnonymous]
        
        public async Task<IActionResult> Login( LoginRequestDTO loginRequestDTO)
        {
            var verify = await unitOfWork.UserRepository.GetUserByEmail(loginRequestDTO.email);

            if (verify.IsVerified == false)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("Your email has not been confirmed");
                return Ok(response);
            }

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
        [AllowAnonymous]
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
                    string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);

                    var cacheKey = $"VerificationCodeFor-{model.email}";
                    memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));
                    //  var codes = memoryCache.Set(model.email, verificationCode);

                    // Send verification email
                    await unitOfWork.EmailService.SendEmailAsync(model.email, "Your Verification Code", $" Hi {model.userName} , Your code is: {verificationCode}");
                    //    await unitOfWork.UserRepository.saveAsync();
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

        [HttpPost("addCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddCode(string email, string code)
        {

            //   var ValedationCode = memoryCache.Get<string>(email);
            if (code == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages.Add("You input code is null , Please enter it correctly");
                return BadRequest(response);
            }
            if (!memoryCache.TryGetValue($"VerificationCodeFor-{email}", out string verificationCode))
            {
                // If the code is not found in the cache, return a 404 Not Found response
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages.Add("No verification code found for the provided email.");
                return NotFound(response);
            }
            if (verificationCode == code)
            {
                memoryCache.Remove($"VerificationCodeFor-{email}");
                var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
                getUser.IsVerified = true;
                await unitOfWork.UserRepository.UpdateUser(getUser);
                await unitOfWork.UserRepository.saveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = "Verification successful, you are registered now.";
                return Ok(response);
            }
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMassages.Add("The code entered is incorrect, please add it correctly");
            return BadRequest(response);
        }

        [HttpGet("reSendCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> reSendTheVerificationCode(string email)
        {
            var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
            if(getUser==null )
            {
                response.ErrorMassages.Add($"The user with email = {email} is not found , register first");
                return response;
            }
            if (getUser.IsVerified == true)
            {
                response.Result = "you are verified your email !!";
                return response;
            }
            string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);
            var cacheKey = $"VerificationCodeFor-{email}";
            memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));                        
            await unitOfWork.EmailService.SendEmailAsync(email, "Your Verification Code", $" Hi {getUser.userName} , Your code is: {verificationCode}");
            response.Result = "We send the new code to your email";
            return response;
        }


        [HttpPut("EditProfile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> EditProfileAsync(int id,[FromForm] ProfileDTO profile)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(response);
            }
            if (profile == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the user is not found" };
                return Ok(response);
            }
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            var profileToUpdate = await unitOfWork.UserRepository.getUserByIdAsync( id);
            if (profileToUpdate == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the user is not found" };
                return NotFound(response);
            }
            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {
                    mapper.Map(profile, profileToUpdate);
                    await unitOfWork.UserRepository.updateSubAdminAsync(profileToUpdate);
                    var success1 = await unitOfWork.UserRepository.saveAsync();
                    var success2 = 0;
                    string imageUrl = "";
                    ProfileDTO profileResult=null;
                    if (profile.image != null)
                        
                    {
                        if ( ! new[] { ".png", ".jpg", ".jpeg" }.Contains(Path.GetExtension(profile.image.FileName).ToLower()))
                        {
                            response.ErrorMassages.Add($"The image extention is not allowd");
                            return response;
                        }
                         imageUrl = "Files\\" +  await unitOfWork.FileRepository.UploadFile1(profile.image);
                    }
                    if (profileToUpdate.role.ToLower() == "admin")
                    {
                        Admin adminToUpdate = await unitOfWork.AdminRepository.GetAdminByIdAsync(id);
                        adminToUpdate.ImageUrl = imageUrl;
                        var adminMapper = mapper.Map(profile, adminToUpdate );
                        await unitOfWork.AdminRepository.updateSubAdminAsync(adminMapper);                        
                        success2 = await unitOfWork.AdminRepository.saveAsync();
                        profileResult = mapper.Map<Admin, ProfileDTO>(adminToUpdate);
                    }
                    else if (profileToUpdate.role.ToLower() == "subadmin" || profileToUpdate.role.ToLower() == "main-subadmin")
                    {
                        SubAdmin subAdminToUpdate = await unitOfWork.SubAdminRepository.GetSubAdminByIdAsync(id);
                        subAdminToUpdate.ImageUrl = imageUrl;
                        var subAdminMapper = mapper.Map(profile, subAdminToUpdate);
                        await unitOfWork.SubAdminRepository.updateSubAdminAsync(subAdminMapper);

                        success2 = await unitOfWork.SubAdminRepository.saveAsync();
                        profileResult = mapper.Map<SubAdmin, ProfileDTO>(subAdminToUpdate);
                    }
                    else if (profileToUpdate.role.ToLower() == "instructor")
                    {
                        Instructor instructorToUpdate = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(id);
                        instructorToUpdate.ImageUrl = imageUrl;
                        var instructorMapper = mapper.Map(profile, instructorToUpdate);
                        await unitOfWork.instructorRepositpry.updateSubAdminAsync(instructorMapper);
                        success2 = await unitOfWork.instructorRepositpry.saveAsync();
                        profileResult = mapper.Map<Instructor, ProfileDTO>(instructorToUpdate);
                    }
                    else if (profileToUpdate.role.ToLower() == "student")
                    {
                        Student StudentToUpdate = await unitOfWork.StudentRepository.getStudentByIdAsync(id);
                        StudentToUpdate.ImageUrl = imageUrl;
                        var studentMapper = mapper.Map(profile, StudentToUpdate);
                        await unitOfWork.StudentRepository.updateSubAdminAsync(studentMapper);                        
                        success2 = await unitOfWork.StudentRepository.saveAsync();
                        profileResult = mapper.Map<Student, ProfileDTO>(StudentToUpdate);
                    }

                    profileResult.FName = profileToUpdate.userName;
                    if (success1 > 0 && success2 > 0)
                    {
                        await transaction.CommitAsync();
                        response.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
                        response.IsSuccess = true;
                        response.Result = profileResult;
                        return Ok(response);
                    }
                    return  BadRequest(response);
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


        [HttpGet("GetProfileInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> GetUserInfo(int id)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(response);
            }

            var UserFound = await unitOfWork.UserRepository.getUserByIdAsync(id); 
            if (UserFound == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the user is not found" };
                return NotFound(response);
            }
            var GetUser =await  unitOfWork.UserRepository.ViewProfileAsync(id, UserFound.role);
            UserInfoDTO usermapper =null;
            if (UserFound.role.ToLower() == "instructor")
            {
                usermapper = mapper.Map<Instructor, UserInfoDTO>(GetUser.instructor);
            }
            else if (UserFound.role.ToLower() == "admin")
            {
                usermapper = mapper.Map<Admin, UserInfoDTO>(GetUser.admin);
            }
            else if (UserFound.role.ToLower() == "subadmin" || UserFound.role.ToLower() =="main-subadmin")
            {
                usermapper = mapper.Map<SubAdmin, UserInfoDTO>(GetUser.subadmin);
            }
            else if (UserFound.role.ToLower() == "student")
            {
                usermapper = mapper.Map<Student, UserInfoDTO>(GetUser.student);
            }
            usermapper.UserId = id;
            if (GetUser == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the user is not found" };
                return NotFound(response);
            }
            if (usermapper.ImageUrl != null)
            {
                CommonClass.ImageTOHttp(usermapper);
            }           
            response.StatusCode=HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result= usermapper;
            return Ok(response);

        }



        [HttpGet("changePassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ApiResponce>> changePassword (int userId , string NewPassword)
        {
            var getUser = await unitOfWork.UserRepository.getUserByIdAsync(userId);
            if(getUser == null)
            {
                response.ErrorMassages.Add("The user is not found");
                return response;
            }
            getUser.password= BCrypt.Net.BCrypt.HashPassword(NewPassword);
            await unitOfWork.UserRepository.UpdateUser(getUser);
            await unitOfWork.UserRepository.saveAsync();             
            response.Result = "The password is changed";
            return Ok(response);
            
            
        }

    }
}
