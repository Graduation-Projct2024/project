using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Repository.GenericRepository;
using courseProject.Services.StudentCourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCoursesServices studentCoursesServices;

        public StudentCourseController(IStudentCoursesServices studentCoursesServices)
        {
            this.studentCoursesServices = studentCoursesServices;
        }



        [HttpPost("EnrollInCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<ActionResult<ApiResponce>> EnrollInCourseAsync(StudentCourseDTO studentCourseDTO)
        {
            var enrolledCourse = await studentCoursesServices.EnrollInCourse(studentCourseDTO);
            if (enrolledCourse.FirstError.Type == ErrorOr.ErrorType.NotFound)
                return NotFound(new ApiResponce { ErrorMassages = enrolledCourse.FirstError.Description });
            else if (enrolledCourse.FirstError.Type == ErrorOr.ErrorType.Validation)
                return Ok(new ApiResponce { ErrorMassages = enrolledCourse.FirstError.Description });

            return Ok(new ApiResponce { Result = "The Request To Join Course Is Created Successfully" });
        }





        [HttpPatch("ApprovelToJoin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "MainSubAdmin")]
        public async Task<ActionResult<ApiResponce>> ApprovalForTheStudentToJoinTheCourse(Guid courseId, Guid studentId, string status)
        {
            var editedStatus = await studentCoursesServices.ApprovelToJoinCourse(courseId, studentId, status);
            if (editedStatus.IsError) return NotFound(new ApiResponce { ErrorMassages = editedStatus.FirstError.Description });
            return Ok(new ApiResponce { Result= "Status updated successfully." });
        }
    }
}
