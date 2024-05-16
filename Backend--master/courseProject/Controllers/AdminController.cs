using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using courseProject.Core.IGenericRepository;
using AutoMapper;
using courseProject.Core.Models.DTO;
using System.Net;
using Microsoft.AspNetCore.Authorization;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiResponce responce;

        public AdminController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            responce=new ApiResponce();
        }

        [HttpPut("EditOnCourseAfterAccredit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Admin")]
        public async Task<ActionResult<ApiResponce>> EditOnCourseAfterAccreditByAdmin(int courseId, [FromForm] EditCourseAfterAccreditDTO editedCourse)
        {
            if(courseId == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode= HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The input course id is null , enter it ");
                return responce;
            }
            try
            {

                var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
                if (getCourse == null)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add($"The course with id = {courseId} is not found ");
                    return responce;
                }
                if (getCourse.status.ToLower() !="accredit")
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotAcceptable;
                    responce.ErrorMassages.Add($"The course with id = {courseId} is not accredit yet ");
                    return responce;
                }
                mapper.Map(editedCourse , getCourse);
                if (getCourse == null)
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.NotFound;
                    responce.ErrorMassages.Add("The all inputs of form is null");
                    return responce;
                }
                getCourse.dateOfUpdated = DateTime.Now;
                if (editedCourse.image != null)
                {
                    getCourse.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(getCourse.image);
                }
                await unitOfWork.SubAdminRepository.updateCourse(getCourse);
                if(await unitOfWork.CourseRepository.saveAsync() > 0)
                {
                    responce.IsSuccess = true;
                    responce.StatusCode = HttpStatusCode.OK;
                    responce.Result = editedCourse;
                    return Ok(responce);
                }
                
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("some error an occured , and the inputs does not saved ") ;
                return BadRequest(responce);
            }
            catch(Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("the error is " + ex + ex.Message);
                return BadRequest(responce);
            }
        }


        [HttpPost("AddSkillOptionsByAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResponce>> AddSkillOptionsByAdmin(string skillName)
        {
            if (skillName == null)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add("The name of skill is null");
                return BadRequest(responce);
            }
            try
            {
                var allSkills = await unitOfWork.AdminRepository.GetAllSkillsAsync();
                if (allSkills.Any(x => x.name == skillName))
                {
                    responce.IsSuccess = false;
                    responce.StatusCode = HttpStatusCode.BadRequest;
                    responce.ErrorMassages.Add("The skill name is exist");
                    return responce;
                }
                Skills skills = new Skills();
                skills.name = skillName;
                await unitOfWork.AdminRepository.addSkillOptionsAsync(skills);
                if (await unitOfWork.AdminRepository.saveAsync() > 0)
                {
                    responce.IsSuccess = true;
                    responce.StatusCode = HttpStatusCode.OK;
                    responce.Result = skillName;
                }
                return responce;
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.StatusCode = HttpStatusCode.BadRequest;
                responce.ErrorMassages.Add($"The error is : {ex.Message}");
                return BadRequest(responce);
            }
        }



    }
}
