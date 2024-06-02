using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Repository.GenericRepository;
using courseProject.Services.Skill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISkillsServices skillsServices;
        private readonly ApiResponce response;
        

        public SkillController(IMapper mapper , ISkillsServices skillsServices)
        {
            this.mapper = mapper;
            this.skillsServices = skillsServices;
            response=new ApiResponce();
           
        }


        [HttpPost("AddSkillOptionsByAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddSkillOptionsByAdmin(string skillName)
        {
            var addSkill = await skillsServices.AddSkillByAdmin(skillName);
            response.Result = addSkill.Value;
            if (addSkill.IsError == true) response.ErrorMassages=addSkill.FirstError.Description;
            return Ok( response);
        }


        [HttpGet("GetAllSkillOptions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Student")]
        public async Task<IActionResult> GetAllOptions()
        {
            
            return Ok( new ApiResponce { Result = await skillsServices.getAllSkillesAddedByAdmin() });

        }


        [HttpGet("GetAllSkillOptionsToInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize("Instructor")]
        public async Task<IActionResult> GetAllOptionsToInstructorDropdown(Guid instructorId)
        {
           
            return Ok( new ApiResponce { Result= await skillsServices.getAllSkillOptionsToInstructor(instructorId) });

        }

        [HttpPost("selectAnInstructorSkills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> SelectASkillsByInstructor(Guid instructorId, [FromForm] ListIntegerDTO array)
        {
            
           var selectNewSkill = await skillsServices.chooseANewSkillToInstructor(instructorId, array);
            if (selectNewSkill.FirstError.Type == ErrorOr.ErrorType.NotFound)
            {
                return NotFound(new ApiResponce { ErrorMassages = (selectNewSkill.FirstError.Description) });

            }
            else if((selectNewSkill.FirstError.Type == ErrorOr.ErrorType.Validation))
                return Ok(new ApiResponce { ErrorMassages = (selectNewSkill.FirstError.Description) });
            return Ok( new ApiResponce { Result= "The skills is added successfully"});

        }



        [HttpDelete("DeleteAnInstructorSkill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<ActionResult<ApiResponce>> DeleteAnInstructorSkillFromSelected(Guid InstructorId, Guid SkillId)
        {
            var deletedSkill = await skillsServices.DeleteAnInstructorSkill(InstructorId, SkillId);
            if (deletedSkill.IsError) return new ApiResponce { ErrorMassages =  deletedSkill.FirstError.Description  };
            return new ApiResponce { Result = "The skill is deleted successfully" };
        }

        [HttpGet("GetAllInstructorSkills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetAllInstructorSkills(Guid instructorId)
        {

            var getAllHisSkills = await skillsServices.GetAllInstructorSkills(instructorId);
            if (getAllHisSkills.IsError) return NotFound( new ApiResponce { ErrorMassages =  getAllHisSkills.FirstError.Description  });
            return Ok( new ApiResponce { Result = getAllHisSkills.Value });
        }

    }
}
