using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using courseProject.Core.Models;


using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private readonly projectDbContext dbContext;

        public AdminController(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[HttpGet]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public ActionResult<IReadOnlyList<Student>> GetAllStudents()
        //{
            
        //    return dbContext.students.ToList();
        //}

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<SubAdmin> CreateSubAdmins(SubAdmin subAdmin)
        {
            if(subAdmin == null)
            {
                return BadRequest();
            }

            if(subAdmin.Id >0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            dbContext.subadmins.Add(subAdmin);
            dbContext.SaveChanges();
            return Ok(subAdmin);

        }

    }
}
