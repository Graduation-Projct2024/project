using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace courseProject.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public UserServices(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }



        public async Task<ErrorOr<User>> getUserById(Guid userId)
        {
            var getUser = await unitOfWork.UserRepository.getUserByIdAsync(userId);
            if (getUser == null) return ErrorUser.NotFound;
            return getUser;
        }

        public async Task<ErrorOr<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            var verify = await unitOfWork.UserRepository.GetUserByEmail(loginRequestDTO.email);

            if (verify.IsVerified == false) return ErrorUser.UnVarified;


            var loginResponse = await unitOfWork.UserRepository.LoginAsync(loginRequestDTO);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
                return ErrorUser.InCorrectInput;
            return loginResponse;
        }

        public async Task<ErrorOr<Created>> Register(RegistrationRequestDTO model)
        {
            if (model.password != model.ConfirmPassword) return ErrorUser.IncorrectPassword;


            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(model.email);

            if (!ifUserIsUniqe) return ErrorUser.ExistEmail;



            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {

                var createdUser = await unitOfWork.UserRepository.RegisterAsync(model);
                var success1 = await unitOfWork.StudentRepository.saveAsync();


                if (model.role.ToLower() == "student")
                {
                    var user = mapper.Map<User, Student>(createdUser);
                    var modelMapped = mapper.Map<Student>(model);
                    modelMapped.StudentId = user.StudentId;
                    await unitOfWork.StudentRepository.CreateStudentAccountAsync(modelMapped);
                }

                var success2 = await unitOfWork.SubAdminRepository.saveAsync();
                string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);

                var cacheKey = $"VerificationCodeFor-{model.email}";
                memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));
                await unitOfWork.EmailService.SendEmailAsync(model.email, "Your Verification Code", $" Hi {model.userName} , Your code is: {verificationCode}");
                if (success1 > 0 && success2 > 0)
                {
                    await transaction.CommitAsync();
                    return Result.Created;
                }

                return ErrorUser.hasError;
            }
        }


        public async Task<ErrorOr<Success>> addCodeVerification(string email, string code)
        {


            if (!memoryCache.TryGetValue($"VerificationCodeFor-{email}", out string verificationCode))
            {
                return ErrorUser.NotFound;
            }
            if (verificationCode == code)
            {
                memoryCache.Remove($"VerificationCodeFor-{email}");
                var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
                getUser.IsVerified = true;
                await unitOfWork.UserRepository.UpdateUser(getUser);
                await unitOfWork.UserRepository.saveAsync();
                return Result.Success;
            }
            return ErrorUser.InCorrectCode;
        }

        public async Task<ErrorOr<Success>> reSendTheVerificationCode(string email)
        {
            var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
            if (getUser == null) return ErrorUser.NotFound;

            if (getUser.IsVerified == true) return ErrorUser.Verified;

            string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);
            var cacheKey = $"VerificationCodeFor-{email}";
            memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));
            await unitOfWork.EmailService.SendEmailAsync(email, "Your Verification Code", $" Hi {getUser.userName} , Your code is: {verificationCode}");
            return Result.Success;
        }

        public async Task<ErrorOr<Updated>> EditUserProfile(Guid id, ProfileDTO profile)
        {

            var profileToUpdate = await unitOfWork.UserRepository.getUserByIdAsync(id);
            if (profileToUpdate == null) return ErrorUser.NotFound;

            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {

                mapper.Map(profile, profileToUpdate);
                await unitOfWork.UserRepository.updateSubAdminAsync(profileToUpdate);
                var success1 = await unitOfWork.UserRepository.saveAsync();
                var success2 = 0;
                string imageUrl = "";
                ProfileDTO profileResult = null;
                if (profile.image != null)

                {
                   
                    imageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(profile.image);
                }
                if (profileToUpdate.role.ToLower() == "admin")
                {
                    Admin adminToUpdate = await unitOfWork.AdminRepository.GetAdminByIdAsync(id);
                    adminToUpdate.ImageUrl = imageUrl;
                    var adminMapper = mapper.Map(profile, adminToUpdate);
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
                    return Result.Updated;
                }
                return ErrorUser.hasError;
            }

        }

        public async Task<ErrorOr<UserInfoDTO>> GetProfileInfo(Guid id)
        {
            var UserFound = await unitOfWork.UserRepository.getUserByIdAsync(id);
            if (UserFound == null) return ErrorUser.NotFound;
         
            var GetUser = await unitOfWork.UserRepository.ViewProfileAsync(id, UserFound.role);
            UserInfoDTO usermapper = null;
            if (UserFound.role.ToLower() == "instructor")
            {
                usermapper = mapper.Map<Instructor, UserInfoDTO>(GetUser.instructor);
            }
            else if (UserFound.role.ToLower() == "admin")
            {
                usermapper = mapper.Map<Admin, UserInfoDTO>(GetUser.admin);
            }
            else if (UserFound.role.ToLower() == "subadmin" || UserFound.role.ToLower() == "main-subadmin")
            {
                usermapper = mapper.Map<SubAdmin, UserInfoDTO>(GetUser.subadmin);
            }
            else if (UserFound.role.ToLower() == "student")
            {
                usermapper = mapper.Map<Student, UserInfoDTO>(GetUser.student);
            }
            usermapper.UserId = id;
          
            if (usermapper.ImageUrl != null)
            {
                CommonClass.ImageTOHttp(usermapper);
            }
            return usermapper;
        }

        public async Task<ErrorOr<Updated>> changePassword(Guid userId, string NewPassword)
        {
            var getUser = await unitOfWork.UserRepository.getUserByIdAsync(userId);
            if (getUser == null) return ErrorUser.NotFound;
            
            getUser.password = BCrypt.Net.BCrypt.HashPassword(NewPassword);
            await unitOfWork.UserRepository.UpdateUser(getUser);
            await unitOfWork.UserRepository.saveAsync();
            return Result.Updated;
        }
    }
}

