using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.ServiceErrors;
using courseProject.Services.Instructors;
using courseProject.Services.SubAdmins;
using ErrorOr;

namespace courseProject.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
   

        public EmployeeServices(IUnitOfWork unitOfWork , IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
        }


        public async Task<IReadOnlyList<EmployeeDto>> getAllEmployees()
        {
            var SubAdmins = await unitOfWork.SubAdminRepository.GetAllEmployeeAsync();
            var instructors = await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();

            var mapperSubAdmin = mapper.Map<IReadOnlyList<SubAdmin>, IReadOnlyList<EmployeeDto>>(SubAdmins);
          
            var mapperInstructor = mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<EmployeeDto>>(instructors);
           
            var allEmployees = (mapperSubAdmin.Concat(mapperInstructor)).OrderByDescending(x => x.dateOfAdded).ToList();
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
           
                var userupdated = mapper.Map<User>(employee);
             
                await unitOfWork.UserRepository.createEmployeeAccount(userupdated);
                await unitOfWork.UserRepository.saveAsync();
                if (employee.role.ToLower() == "subadmin" || employee.role.ToLower() == "main-subadmin")
                {
                    var modelMapped = mapper.Map<SubAdmin>(employee);
                
                    modelMapped.SubAdminId = userupdated.UserId;
                    await unitOfWork.SubAdminRepository.createSubAdminAccountAsync(modelMapped);
                }
                else if (employee.role.ToLower() == "instructor")
                {
                    var modelMapped = mapper.Map<Instructor>(employee);
               
                    modelMapped.InstructorId = userupdated.UserId;
                    await unitOfWork.instructorRepositpry.createInstructorAccountAsync(modelMapped);
                }
                var success2 = await unitOfWork.SubAdminRepository.saveAsync();



                if ( success2 > 0)
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
               
                employee= mappedEmployee;
            }
            if (getInstructor == null && UserToGet.role.ToLower() == "instructor") return ErrorInstructor.NotFound;

            else if (getInstructor != null && UserToGet.role.ToLower() == "instructor")
            {
                
                var mappedEmployee = mapper.Map<Instructor, EmployeeDto>(getInstructor);
               
                employee= mappedEmployee;
            }
            return employee;



        }

        public async Task<ErrorOr<Updated>> UpdateEmployeeFromAdmin(Guid employeeId, EmployeeForUpdateDTO employee)
        {
           
            var UserToUpdate = await unitOfWork.UserRepository.getUserByIdAsync(employeeId);

            if (UserToUpdate == null && (UserToUpdate.role.ToLower() == "subadmin" || UserToUpdate.role.ToLower() == "main-subadmin"))
                return ErrorSubAdmin.NotFound;
            if (UserToUpdate == null && UserToUpdate.role.ToLower() == "instructor")
                return ErrorInstructor.NotFound;
       
                var userMapper = mapper.Map(employee, UserToUpdate);
          
                await unitOfWork.UserRepository.UpdateUser(userMapper);
                    var success1 = await unitOfWork.UserRepository.saveAsync();
               
                         return Result.Updated;
               
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
