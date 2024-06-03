using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.ServiceErrors;
using courseProject.Services.Instructors;
using courseProject.Services.SubAdmins;
using ErrorOr;
using System;
using System.Net;

namespace courseProject.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISubAdminServices subAdminServices;
        private readonly IinstructorServices instructorServices;

        public EmployeeServices(IUnitOfWork unitOfWork , IMapper mapper , ISubAdminServices subAdminServices , IinstructorServices instructorServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.subAdminServices = subAdminServices;
            this.instructorServices = instructorServices;
        }


        public async Task<IReadOnlyList<EmployeeDto>> getAllEmployees()
        {
            var SubAdmins = await unitOfWork.SubAdminRepository.GetAllEmployeeAsync();
            var instructors = await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();

            var mapperSubAdmin = mapper.Map<IReadOnlyList<SubAdmin>, IReadOnlyList<EmployeeDto>>(SubAdmins);
            //foreach (var employee in mapperSubAdmin)
            //{
            //    if (employee.ImageUrl != null)
            //    {
            //        employee.ImageUrl = Url + employee.ImageUrl;
            //    }
            //}
          //  CommonClass.EditImageInEmployeeDTO(mapperSubAdmin);
            var mapperInstructor = mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<EmployeeDto>>(instructors);
          //  CommonClass.EditImageInEmployeeDTO(mapperInstructor);
            var allEmployees = (mapperSubAdmin.Concat(mapperInstructor)).OrderBy(x => x.Id).ToList();
            foreach (var employee in allEmployees)
            {
                if (employee.ImageUrl != null)
                {
                    employee.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(employee.ImageUrl);
                }
            }
            return allEmployees;
        }



        public async Task<ErrorOr<Created>> CreateEmployee(EmployeeForCreate employee)
        {
            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(employee.email);

            if (!ifUserIsUniqe) return ErrorUser.ExistEmail;

            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                var userMapped = mapper.Map<RegistrationRequestDTO>(employee);
                var Usermapp = await unitOfWork.UserRepository.RegisterAsync(userMapped);
                Usermapp.IsVerified = true;
                var success1 = await unitOfWork.SubAdminRepository.saveAsync();
                if (employee.role.ToLower() == "subadmin" || employee.role.ToLower() == "main-subadmin")
                {
                    var modelMapped = mapper.Map<SubAdmin>(employee);
                    var userMap = mapper.Map<User, SubAdmin>(Usermapp);
                    modelMapped.SubAdminId = userMap.SubAdminId;
                    await unitOfWork.SubAdminRepository.createSubAdminAccountAsync(modelMapped);
                }
                else if (employee.role.ToLower() == "instructor")
                {
                    var modelMapped = mapper.Map<Instructor>(employee);
                    var userMap = mapper.Map<User, Instructor>(Usermapp);
                    modelMapped.InstructorId = userMap.InstructorId;
                    await unitOfWork.instructorRepositpry.createInstructorAccountAsync(modelMapped);
                }
                var success2 = await unitOfWork.SubAdminRepository.saveAsync();



                if (success1 > 0 && success2 > 0)
                {
                    await transaction.CommitAsync();
                    return Result.Created;
                }

                return ErrorEmployee.hasError;
            }
        }

        

        public async Task<ErrorOr<object>> GetEmployeeById(Guid id)
        {
            
             var getSubAdmin = await unitOfWork.SubAdminRepository.GetSubAdminByIdAsync(id);
            var getInstructor = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(id);
            var UserToGet = await unitOfWork.UserRepository.getUserByIdAsync(id);
            if (UserToGet == null) return ErrorUser.NotFound;

            object employee=null;
            if (getSubAdmin == null && (UserToGet.role.ToLower() == "subadmin"|| UserToGet.role.ToLower() == "main-subadmin")) return ErrorSubAdmin.NotFound;
            
            else if (getSubAdmin != null && (UserToGet.role.ToLower() == "subadmin" || UserToGet.role.ToLower() == "main-subadmin"))
            {
               
                var mappedEmployee = mapper.Map<SubAdmin, EmployeeDto>(getSubAdmin);
               // mappedEmployee.type = "SubAdmin";
                employee= mappedEmployee;
            }
            if (getInstructor == null && UserToGet.role.ToLower() == "instructor") return ErrorInstructor.NotFound;

            else if (getInstructor != null && UserToGet.role.ToLower() == "instructor")
            {
                
                var mappedEmployee = mapper.Map<Instructor, EmployeeDto>(getInstructor);
               // mappedEmployee.type = "Instructor";
                employee= mappedEmployee;
            }
            return employee;



        }

        public async Task<ErrorOr<Updated>> UpdateEmployeeFromAdmin(Guid employeeId, EmployeeForUpdateDTO employee)
        {
            var subAdminToUpdate = await unitOfWork.SubAdminRepository.getSubAdminByIdAsync(employeeId);
            var instructorToUpdate = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(employeeId);
            var UserToUpdate = await unitOfWork.UserRepository.getUserByIdAsync(employeeId);

            if (subAdminToUpdate == null && (UserToUpdate.role.ToLower() == "subadmin" || UserToUpdate.role.ToLower() == "main-subadmin"))
                return ErrorSubAdmin.NotFound;
            if (instructorToUpdate == null && UserToUpdate.role.ToLower() == "instructor")
                return ErrorInstructor.NotFound;
            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
               // unitOfWork.UserRepository.DetachEntity(UserToUpdate);
                var userMapper = mapper.Map(employee, UserToUpdate);
                userMapper.UserId = employeeId;
                userMapper.role = UserToUpdate.role;
                userMapper.password = UserToUpdate.password;


                await unitOfWork.UserRepository.UpdateUser(userMapper);
                    var success1 = await unitOfWork.UserRepository.saveAsync();
                    var success2 = 0;
                    SubAdmin? Subadminmapper = null;
                    Instructor? Instructormapper = null;
                    if (UserToUpdate.role.ToLower() == "subadmin" || UserToUpdate.role.ToLower() == "main-subadmin")
                    {
                    // Detach existing SubAdmin entity
                  //  unitOfWork.SubAdminRepository.DetachEntity(subAdminToUpdate);

                    Subadminmapper = mapper.Map<EmployeeForUpdateDTO, SubAdmin>(employee);
                        Subadminmapper.SubAdminId = subAdminToUpdate.SubAdminId;
                        await unitOfWork.SubAdminRepository.updateSubAdminAsync(Subadminmapper);

                   
                    }

                    if (UserToUpdate.role.ToLower() == "instructor")
                    {
                    // Detach existing Instructor entity
                 //   unitOfWork.instructorRepositpry.DetachEntity(instructorToUpdate);
                    Instructormapper = mapper.Map(employee, instructorToUpdate);
                        Instructormapper.InstructorId = instructorToUpdate.InstructorId;
                        await unitOfWork.instructorRepositpry.updateSubAdminAsync(Instructormapper);

                   
                }
                    success2 = await unitOfWork.SubAdminRepository.saveAsync();
                    if (success1 > 0 && success2 > 0)
                    {
                        await transaction.CommitAsync();
                        
                         return Result.Updated;
                }
                return ErrorEmployee.hasError;
                }
        }



        public async Task<ErrorOr<Updated>> EditRole(Guid userId, string role)
        {
            var getUser = await unitOfWork.UserRepository.getUserByIdAsync(userId);
            if (getUser == null) return ErrorUser.NotFound;

            if (getUser.role.ToLower() != "subadmin" && getUser.role.ToLower() != "main-subadmin")
                return ErrorEmployee.InvalidRole;
            if (role.ToLower() != "subadmin" && role.ToLower() != "main-subadmin")
                return ErrorEmployee.InvalidRole;
            var getAllMainSubAdmins = await unitOfWork.UserRepository.getAllMainSubAmdinRole();
            if (getAllMainSubAdmins.Count() >= 1 && role.ToLower() == "main-subadmin")
                return ErrorEmployee.existsMainSub;
            getUser.role = role.ToLower();
            await unitOfWork.SubAdminRepository.editRole(getUser);
            await unitOfWork.UserRepository.saveAsync();

            return Result.Updated;
        }

       
    }
}
