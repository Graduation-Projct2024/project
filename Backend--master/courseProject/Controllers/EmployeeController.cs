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
using courseProject.core.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly projectDbContext dbContext;
        private readonly IUnitOfWork unitOfWork;
        //private readonly IPagenation<T> pagenation;
        private readonly IGenericRepository1<SubAdmin> subAdminRepo;
        private readonly IGenericRepository1<Instructor> instructorRepo;
        private readonly IGenericRepository1<User> userRepo;
        private readonly IMapper mapper;
        protected ApiResponce responce;
        private Common.CommonClass CommonClass;


        public EmployeeController(projectDbContext dbContext, IUnitOfWork unitOfWork,  IMapper mapper)
        {
            this.dbContext = dbContext;
            this.unitOfWork = unitOfWork;
           // this.pagenation = pagenation;
            this.userRepo = userRepo;
            this.mapper = mapper;
            responce = new ApiResponce();
            CommonClass = new Common.CommonClass();
        }

        [HttpGet("GetAllEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // [Authorize(Policy = "Admin")]
       // [Authorize(Policy = "Admin&subAdmin")]
        public async Task<ActionResult<IReadOnlyList<SubAdmin>>> GetAllEmployeeAsync([FromQuery] PaginationRequest paginationRequest)
        {
            // pagenation.PageSize = pageSize ?? pagenation.PageSize;
            try
            {


                var SubAdmins = await unitOfWork.SubAdminRepository.GetAllEmployeeAsync();

                var instructors = await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();
                if (SubAdmins == null && instructors == null)
                {
                    return NotFound();
                }
                var mapperSubAdmin = mapper.Map<IReadOnlyList<SubAdmin>, IReadOnlyList<EmployeeDto>>(SubAdmins);
                //foreach (var SubAdmin in mapperSubAdmin)
                //{
                //    SubAdmin.type = "SubAdmin";
                //}
                var mapperInstructor = mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<EmployeeDto>>(instructors);
                //foreach (var instructor in mapperInstructor)
                //{
                //    instructor.type = "Instructor";
                //}
                var allEmployees = (mapperSubAdmin.Concat(mapperInstructor)).OrderBy(x=>x.Id).ToList();
           //     allEmployees = allEmployees.OrderBy(x => x.Id);
                responce.Result = (Pagination<EmployeeDto>.CreateAsync(allEmployees, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode=HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("the error occured is :"+ex + "" + ex.Message);
                return BadRequest(responce);
            }

        }


        [HttpGet("GetAllEmployeeForContact")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllEmployeeForContactAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var subAdmins = await unitOfWork.SubAdminRepository.GetAllEmployeeForContactAsync();
            var instructors = await unitOfWork.instructorRepositpry.GetAllEmployeeForContactAsync();

            if (subAdmins == null && instructors == null)
            {
                return NotFound();
            }
            var mapperSubAdmin = mapper.Map<IReadOnlyList<SubAdmin>, IReadOnlyList<ContactDto>>(subAdmins);
            var mapperInstructor = mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<ContactDto>>(instructors);
            var allEmployees = (mapperSubAdmin.Concat(mapperInstructor)).ToList();
            responce.IsSuccess = true;
            responce.Result = (Pagination<ContactDto>.CreateAsync(allEmployees, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            return Ok(responce);
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
                    if (model.role.ToLower() == "subadmin" || model.role.ToLower()=="main-subadmin")
                    {
                        var modelMapped = mapper.Map<SubAdmin>(model);
                        var userMap = mapper.Map<User, SubAdmin>(Usermapp);
                        modelMapped.SubAdminId = userMap.SubAdminId;
                        await unitOfWork.SubAdminRepository.createSubAdminAccountAsync(modelMapped);
                    } 
                    else if(model.role.ToLower() == "instructor")
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
            var subAdminToGet = await unitOfWork.SubAdminRepository.GetSubAdminByIdAsync(id);
            var instructorToGet = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(id);
            var UserToGet = await unitOfWork.UserRepository.getUserByIdAsync( id);
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


        [HttpPut("UpdateEmployeeFromAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> updateEmployee(int id,[FromForm] EmployeeForUpdateDTO EmpolyeeModel)
        {

            if (id <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(responce);
            }

            if (EmpolyeeModel == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { "the subAdmin is not found" };
                return NotFound(responce);
            }
            if (id != EmpolyeeModel.Id || !ModelState.IsValid) {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(responce);
            }           
            var subAdminToUpdate = await unitOfWork.SubAdminRepository.getSubAdminByIdAsync(id);
            var instructorToUpdate = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(id);
            var UserToUpdate = await unitOfWork.UserRepository.getUserByIdAsync(id);
            if (subAdminToUpdate == null && UserToUpdate.role.ToLower() == "subadmin") {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { "the subAdmin is not found" };
                return NotFound(responce);
            }
            if (instructorToUpdate == null && UserToUpdate.role.ToLower() == "instructor")
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages = new List<string>() { "the instructor is not found" };
                return NotFound(responce);
            }
            using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
            {
                try
                {

                    var userMapper = mapper.Map(EmpolyeeModel, UserToUpdate);
                    userMapper.UserId = id;
                    userMapper.role = UserToUpdate.role;
                    userMapper.password = UserToUpdate.password;
                    await unitOfWork.UserRepository.updateSubAdminAsync(userMapper);
                    var success1 = await unitOfWork.UserRepository.saveAsync();
                    var success2 = 0;
                    SubAdmin? Subadminmapper = null;
                    Instructor? Instructormapper = null;
                    if (UserToUpdate.role.ToLower() == "subadmin")
                    {
                         Subadminmapper = mapper.Map<EmployeeForUpdateDTO, SubAdmin>(EmpolyeeModel);
                         Subadminmapper.SubAdminId = subAdminToUpdate.SubAdminId;
                         await subAdminRepo.updateSubAdminAsync(Subadminmapper);
                        responce.Result = Subadminmapper;
                    }

                    if (UserToUpdate.role.ToLower() == "instructor")
                    {
                         Instructormapper = mapper.Map(EmpolyeeModel, instructorToUpdate);
                        Instructormapper.InstructorId = instructorToUpdate.InstructorId;
                         await unitOfWork.instructorRepositpry.updateSubAdminAsync(Instructormapper);
                        responce.Result = Instructormapper;
                    }
                    success2 = await unitOfWork.SubAdminRepository.saveAsync();
                    if (success1 > 0 && success2 > 0)
                    {
                        await transaction.CommitAsync();
                        responce.StatusCode = HttpStatusCode.OK;
                        responce.IsSuccess = true;
                        return Ok(responce);
                    }
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.IsSuccess = false;
                    responce.Result = null;
                    return BadRequest(responce);
                }  
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.IsSuccess = false;
                    responce.Result = null;
                    return BadRequest(responce);
                } 
            }
        }


        [HttpGet("GetAllCoursesGivenByInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllCoursesByInstructorId (int Instructorid , [FromQuery] PaginationRequest paginationRequest)
        {
            if (Instructorid <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(responce);
            }
            var courseFond = await unitOfWork.instructorRepositpry.GetAllCoursesGivenByInstructorIdAsync(Instructorid);
            if (courseFond.Count() == 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NoContent;              
                responce.ErrorMassages = new List<string>() { $"The Instructor Of Id = {Instructorid} Does Not Teach Any Courses " };
                return  Ok(responce);
            }
            CommonClass.EditImageInFor(courseFond, null);
            responce.StatusCode=HttpStatusCode.OK;
            responce.IsSuccess = true;
            responce.Result=(Pagination<Course>.CreateAsync(courseFond , paginationRequest.pageNumber , paginationRequest.pageSize)).Result;
            return Ok(responce);
        }


        [HttpPost("AddInstructorOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddOfficeHours(int InstructorId ,[FromForm] WorkingHourDTO _Working_Hours)
        {
            if(InstructorId <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The Id is less or equal 0 ");
                return BadRequest(responce);
            }            
            if(_Working_Hours == null)
            {
                responce.IsSuccess=false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The Input is null !");
                return BadRequest(responce);
            }
            var instructorFound = await unitOfWork.instructorRepositpry.GetEmployeeById(InstructorId);
            if(instructorFound == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The Instructor Id = {InstructorId} is not found ");
                return BadRequest(responce);
            }
            if(! CommonClass.IsValidTimeFormat(_Working_Hours.startTime) || !CommonClass.IsValidTimeFormat(_Working_Hours.endTime))
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The Input Time Has An Error");
                return BadRequest(responce);
            }
            var OfficeHourMapper = mapper.Map<WorkingHourDTO, Instructor_Working_Hours>(_Working_Hours);
            if(!CommonClass.CheckStartAndEndTime(OfficeHourMapper.startTime , OfficeHourMapper.endTime))
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("Your Input of Start Time Is Greater Than End Time !");
                return BadRequest(responce);
            }
            OfficeHourMapper.InstructorId = InstructorId;
            await unitOfWork.instructorRepositpry.AddOfficeHoursAsync(OfficeHourMapper);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            if (success > 0)
                {
                    responce.IsSuccess = true;
                    responce.StatusCode = HttpStatusCode.Created;
                    responce.Result = _Working_Hours;
                    return Ok(responce);
                }
            responce.IsSuccess = false;
            responce.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(responce);

        }


        [HttpGet("GetInstructorOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetInstructorOfficeHourById(int Instructorid)
        {
            if(Instructorid <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The Id is less or equal 0 ");
                return BadRequest(responce);
            }
            var instructorFound = await unitOfWork.instructorRepositpry.GetEmployeeById(Instructorid);
            if (instructorFound == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The Instructor with Id = {Instructorid} is not found ");
                return BadRequest(responce);
            }
            var InstructorOfficeHours = await unitOfWork.instructorRepositpry.GetOfficeHourByIdAsync(Instructorid);
            var InstructorOfficeHoursMapper = mapper.Map<IReadOnlyList< Instructor_Working_Hours>,IReadOnlyList< GetWorkingHourDTO>>(InstructorOfficeHours);
            if (InstructorOfficeHours.Count <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages.Add($"The Instructor with Id = {Instructorid} is not add his office hour ");
                return NotFound(responce);
            }
            responce.StatusCode=HttpStatusCode.OK;
            responce.IsSuccess=true;
            responce.Result = InstructorOfficeHoursMapper;
            return Ok(responce);

        }


        [HttpGet("GetAllSubmissionForTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllSubmissionUsingTaskId(int taskId , [FromQuery] PaginationRequest paginationRequest)
        {
            if (taskId <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode= HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The task id is less or equal 0");
                return BadRequest(responce);
            }
            var taskFound = await unitOfWork.materialRepository.GetMaterialByIdAsync(taskId);
            if (taskFound== null)
            {
                responce.IsSuccess = false;
                responce.StatusCode=HttpStatusCode.NotFound;
                responce.ErrorMassages.Add($"The task with id = {taskId} is not found ");
                return NotFound(responce );
            }
            if(taskFound.type.ToLower() != "task")
            {
                responce.IsSuccess = false;
                responce.StatusCode= HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"This material with id = {taskId} is not task , and it is not has a submissions");
                return BadRequest(responce);
            }
            var GetSubmissions = await unitOfWork.instructorRepositpry.GetAllSubmissionsByTaskIdAsync(taskId);
            if(GetSubmissions.Count() == 0)
            {
                responce.IsSuccess = false;
                responce.ErrorMassages.Add("Not submission added yet");
                return Ok(responce);
            }
            var submissionMapper = mapper.Map<IReadOnlyList<Student_Task_Submissions> , IReadOnlyList<StudentSubmissionDTO>>(GetSubmissions);
            responce.IsSuccess= true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result =  (Pagination<StudentSubmissionDTO>.CreateAsync(submissionMapper , paginationRequest.pageNumber , paginationRequest.pageSize)).Result;
            return Ok( responce );
        }


        [HttpGet("GetAllCustomCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //[Authorize(Policy ="MainSubAdmin")]
        public async Task<ActionResult<ApiResponce>> GetAllCustomCoursesToMainSubAdmin([FromQuery] PaginationRequest paginationRequest)
        {
            try
            {


                var GetCustomCourse = await unitOfWork.SubAdminRepository.GerAllCoursesRequestAsync();
                if (GetCustomCourse.Count() == 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add("No Request To Custom Course Yet");
                    return NotFound(responce);
                }
                var CustomCoursesMapper = mapper.Map<IReadOnlyList<Request>, IReadOnlyList<CustomCourseForRetriveDTO>>(GetCustomCourse);

                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                responce.Result = (Pagination<CustomCourseForRetriveDTO>.CreateAsync(CustomCoursesMapper, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode=HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("the error is :" + ex.Message);
                return BadRequest(responce);
            }
        }


        [HttpGet("GetCustomCoursesById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetCustomCourseById(int id)
        {
            if (id <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages.Add("The Custom COurse id is less or equal 0");
                return NotFound(responce);
            }
            var GetCustomCourse = await unitOfWork.SubAdminRepository.GerCourseRequestByIdAsync(id);
            if (GetCustomCourse == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages.Add($"The custom course with id = {id} is Not found");
                return NotFound(responce);
            }
            var CustomCoursesMapper = mapper.Map<Request, CustomCourseForRetriveDTO>(GetCustomCourse);

            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result = CustomCoursesMapper;
            return Ok(responce);

        }

        [HttpGet("GetAllLectureRequest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<ApiResponce>> GetAllLecturesByInstructorId(int instructorId , [FromQuery] PaginationRequest paginationRequest)
        {
            if (instructorId <= 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The Instructor Id Is Less Or Equal 0 ");
                return BadRequest( responce );
            }

            var instructorFound = await unitOfWork.instructorRepositpry.GetEmployeeById(instructorId);
            if(instructorFound == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages.Add($"The Instructor With Id ={instructorId} Is Not Found , Make Sure The Id");
                return NotFound( responce );
            }
            var GetLectures = await unitOfWork.instructorRepositpry.GetAllConsultationRequestByInstructorIdAsync(instructorId);
            if(GetLectures.Count() == 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode= HttpStatusCode.NoContent;
              //  responce.ErrorMassages.Add("No Consultation Lectures For This Instructor");
                Response.Headers.Add("Message", "No Consultation Lectures For This Instructor");
                return  NoContent();
            }
            var LecturesMapper = mapper.Map<IReadOnlyList<Consultation>, IReadOnlyList<LecturesForRetriveDTO>>(GetLectures);
            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result = (Pagination<LecturesForRetriveDTO>.CreateAsync(LecturesMapper, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            return Ok(responce);

        }


        [HttpGet("GetAllInstructorsList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetInstructorsForDropDownList()
        {
            var GetInstructors = await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();
            if (GetInstructors.Count() == 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NotFound;
                responce.ErrorMassages.Add("Not have any instructor account yet ");
                return NotFound(responce);
            }
            var CustomCoursesMapper = mapper.Map<IEnumerable<Instructor>, IEnumerable<EmployeeListDTO>>(GetInstructors);

            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result = CustomCoursesMapper;
            return Ok(responce);
        }


        [HttpGet("GetAllInstructorsOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllIinstructorsWithAllOfficeHours()
        {
            var AllOfficeHours = await unitOfWork.instructorRepositpry.getAllInstructorsOfficeHoursAsync();
            if (AllOfficeHours.Count() == 0)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.NoContent;
                var message = "Not have any instructor account yet ";
                responce.ErrorMassages.Add(message);
                Response.Headers.Add("Message", message);
                return NoContent();
            }
            var InstrctorOfficeHoursMapper = mapper.Map<IReadOnlyList<Instructor_Working_Hours>, IReadOnlyList<Instructor_OfficeHoursDTO>>(AllOfficeHours);
            responce.IsSuccess = true;
            responce.StatusCode = HttpStatusCode.OK;
            responce.Result = InstrctorOfficeHoursMapper;
            return Ok(responce);
        }

        [HttpGet("GetAllRequestToJoinCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
       // [Authorize(Policy ="MainSubAdmin")]
        public async Task<ActionResult<ApiResponce>> GetAllRequestFromStudentsToJoinCourses([FromQuery] PaginationRequest paginationRequest)
        {
            try
            {
                var getRequests = await unitOfWork.SubAdminRepository.getAllRequestToJoindCourseAsync();
                if (getRequests.Count() == 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NoContent;
                    responce.ErrorMassages.Add("There is no new request");
                    return responce;
                }
                var requestMapper = mapper.Map<IReadOnlyList<StudentCourse>, IReadOnlyList<ViewTheRequestOfJoindCourseDTO>>(getRequests);
                responce.IsSuccess = true;
                responce.StatusCode= HttpStatusCode.OK;
                responce.Result= (Pagination<ViewTheRequestOfJoindCourseDTO>.CreateAsync(requestMapper, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The error is :" + ex +ex.Message);
                return BadRequest( responce) ;
            }
        }


        [HttpGet("GetAllSkillOptions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllOptions()
        {
            try
            {
                var allSkills = await unitOfWork.AdminRepository.GetAllSkillsAsync();
                if (allSkills.Count() == 0)
                {
                    responce.IsSuccess = true;
                    responce.StatusCode = HttpStatusCode.NoContent;
                    responce.ErrorMassages.Add("There is no skills yet");
                    return Ok(responce);
                }
                responce.IsSuccess= true;
                responce.StatusCode=HttpStatusCode.OK;
                responce.Result = allSkills;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode=HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The error is : " + ex.Message);
                return BadRequest(responce);
            }
        }

        [HttpGet("GetAllSkillOptionsToInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllOptionsToInstructorDropdown(int instructorId)
        {
            try
            {
                var allSkills = await unitOfWork.instructorRepositpry.getAllUnregisteredSkillsOfTheInstructor(instructorId);
                if (allSkills.Count() == 0)
                {
                    responce.IsSuccess = true;
                    responce.StatusCode = HttpStatusCode.NoContent;
                    responce.ErrorMassages.Add("There is no skills yet");
                    return Ok(responce);
                }
                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                responce.Result = allSkills;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The error is : " + ex.Message);
                return BadRequest(responce);
            }
        }

        [HttpPost("selectAnInstructorSkills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
      //  [Authorize(Policy ="Instructor")]
        public async Task<ActionResult<ApiResponce>> SelectASkillsByInstructor(int instructorId ,[FromForm] ListIntegerDTO array)
        {
            try
            {
                if (instructorId <= 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages.Add("The instructor id is less or equal 0");
                    return BadRequest(responce);
                }
                var FoundInstrutor = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(instructorId);
                if (FoundInstrutor == null)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add($"The instructor with id = {instructorId} is not foud");
                    return NotFound(responce);
                }
                if (array == null || array.skills.Count()==0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NoContent;
                    responce.ErrorMassages.Add("there is no added skills");
                    return (responce);
                }
                await unitOfWork.instructorRepositpry.AddListOfSkillsAsync(instructorId, array.skills);
               // var success = await unitOfWork.instructorRepositpry.saveAsync();
                var getAllSkills = await unitOfWork.instructorRepositpry.GetAllSkillsNameToInstructorAsync(array.skills);
                //if (success > 0)
                //{
                    responce.IsSuccess = true;
                    responce.StatusCode = HttpStatusCode.Created;
                    responce.Result = getAllSkills;
                    return Ok(responce);
            //}
            //    responce.IsSuccess = false;
            //responce.StatusCode = HttpStatusCode.BadRequest;
            //return BadRequest(responce);
        }
            catch(Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The error is : " + ex.Message);
                return BadRequest(responce);
            }
        }


        [HttpPost("GetListOfInstructorForLectures")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAListOfInstrcutorsForBookALectures(int skillId , string startTime , string endTime , DateTime date)
        {
            try
            {
                if (!CommonClass.IsValidTimeFormat(startTime) || !CommonClass.IsValidTimeFormat(endTime) /*|| !CommonClass.IsValidTimeFormat(bookALecture.Duration)*/)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages.Add("The Input Time Has An Error");
                    return BadRequest(responce);
                }
                TimeSpan StartTime = CommonClass.ConvertToTimeSpan(startTime);
                TimeSpan EndTime = CommonClass.ConvertToTimeSpan(endTime);
                if (StartTime >= EndTime)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages.Add("The start time is greater then end time , edit it ");
                    return (responce);
                }
                if ((EndTime - StartTime) > TimeSpan.Parse("02:00") || (EndTime - StartTime) < TimeSpan.Parse("00:30"))
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages.Add("The period entered is greater or less than the permissible period. The permissible period is from 30 minutes to 2 hours");
                    return (responce);
                }
                var getAllSkills = await unitOfWork.AdminRepository.GetAllSkillsAsync();
                if (getAllSkills.Count()==0 || !getAllSkills.Any(x=>x.Id==skillId))
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add($"The skill with id = {skillId} is not found ");
                    return BadRequest(responce);
                }
                var getInstructors = await unitOfWork.instructorRepositpry.getAListOfInstructorDependOnSkillsAndOfficeTime(skillId, StartTime, EndTime, date);
                if (getInstructors.Count() == 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NoContent;
                    responce.Result = "No any instructor is available depends in your inputs";
                    return (responce);
                }
                var instructorMapper = mapper.Map<IReadOnlyList< Instructor_Working_Hours>,IReadOnlyList< EmployeeListDTO>>(getInstructors);
                responce.IsSuccess = true;
                responce.StatusCode = HttpStatusCode.OK;
                responce.Result = instructorMapper;
                return Ok(responce);

            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add(ex.Message);
                return BadRequest(responce);
            }
        }

        [HttpDelete("DeleteAnInstructorSkill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> DeleteAnInstructorSkillFromSelected (int InstructorId , int SkillId)
        {
            try
            {
                if (InstructorId <= 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages.Add("The instructor id is less or equal 0");
                    return BadRequest(responce);
                }
                if (SkillId <= 0)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages.Add("The skill id is less or equal 0");
                    return BadRequest(responce);
                }
                var getAllSkills = await unitOfWork.AdminRepository.GetAllSkillsAsync();
                if (!getAllSkills.Any(x => x.Id == SkillId))
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add($"The skill with id ={SkillId} is not found");
                    return NotFound(responce);
                }
                var instructorFound = await unitOfWork.UserRepository.ViewProfileAsync(InstructorId, "instructor");
                if (instructorFound == null)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add($"The instructor with id ={InstructorId} is not found");
                    return NotFound(responce);
                }
                var getInstructorSkillsRecords = await unitOfWork.instructorRepositpry.GetAllInstructorSkillsRecoredsAsync();
                if(! getInstructorSkillsRecords.Any(x=>x.InstructorId==InstructorId && x.skillId== SkillId))
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add($"The instructor with id ={InstructorId} is not has the skill with id = {SkillId}");
                    return NotFound(responce);
                }
                var name = getAllSkills.FirstOrDefault(x => x.Id == SkillId).name;
                InstructorSkills instructorSkills = new InstructorSkills();
                instructorSkills.InstructorId = InstructorId;
                instructorSkills.skillId = SkillId;
                await unitOfWork.instructorRepositpry.RemoveASkill(instructorSkills);
                if(await unitOfWork.instructorRepositpry.saveAsync() > 0)
                {
                    responce.IsSuccess=true;
                    responce.StatusCode=HttpStatusCode.OK;
                    responce.Result = $"The skill {name} is removed";
                    return Ok(responce);
                }
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("some error an occurred " );
                return BadRequest(responce);

            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The error is : " + ex.Message);
                return BadRequest(responce);
            }
        }



        [HttpGet("GetAllInstructorSkills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> GetAllInstructorSkills(int instructorId)
        {
            if (instructorId <= 0)
            {
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("the instructor id is less or equal 0");
                return BadRequest(responce);
            }
            var getAllHisSkills = await unitOfWork.instructorRepositpry.getAllInstructorSkills(instructorId);
            responce.Result = getAllHisSkills;
            return Ok(responce);
        }


    }
}
