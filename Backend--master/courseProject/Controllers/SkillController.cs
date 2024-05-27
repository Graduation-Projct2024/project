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
        private readonly CommonClass CommonClass;

        public SkillController(IMapper mapper , ISkillsServices skillsServices)
        {
            this.mapper = mapper;
            this.skillsServices = skillsServices;
            response=new ApiResponce();
            CommonClass = new CommonClass();
        }


        [HttpPost("AddSkillOptionsByAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResponce>> AddSkillOptionsByAdmin(string skillName)
        {
            var addSkill = await skillsServices.AddSkillByAdmin(skillName);
            response.Result = addSkill.Value;
            if (addSkill.IsError == true) response.ErrorMassages=addSkill.FirstError.Description;
            return response;
        }


        [HttpGet("GetAllSkillOptions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Student")]
        public async Task<ActionResult<ApiResponce>> GetAllOptions()
        {
            
            return new ApiResponce { Result = await skillsServices.getAllSkillesAddedByAdmin() };

        }


        [HttpGet("GetAllSkillOptionsToInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //[Authorize("Instructor")]
        public async Task<ActionResult<ApiResponce>> GetAllOptionsToInstructorDropdown(Guid instructorId)
        {
           
            return new ApiResponce { Result= await skillsServices.getAllSkillOptionsToInstructor(instructorId) };

        }

        [HttpPost("selectAnInstructorSkills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        //[Authorize(Policy = "Instructor")]
        public async Task<ActionResult<ApiResponce>> SelectASkillsByInstructor(Guid instructorId, [FromForm] ListIntegerDTO array)
        {
            
           var selectNewSkill = await skillsServices.chooseANewSkillToInstructor(instructorId, array);
            if (selectNewSkill.IsError)
            {
               return new ApiResponce { ErrorMassages =  (selectNewSkill.FirstError.Description)  };
                
            }
            return new ApiResponce { Result= "The skills is added successfully"};

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
        //[Authorize]
        public async Task<ActionResult<ApiResponce>> GetAllInstructorSkills(Guid instructorId)
        {

            var getAllHisSkills = await skillsServices.GetAllInstructorSkills(instructorId);
            if (getAllHisSkills.IsError) return new ApiResponce { ErrorMassages =  getAllHisSkills.FirstError.Description  };
            return new ApiResponce { Result = getAllHisSkills.Value };
        }

    }
}
