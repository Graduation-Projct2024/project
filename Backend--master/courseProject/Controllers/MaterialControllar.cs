using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using courseProject.Repository.Data;
using System.Linq;
using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using courseProject.Repository.GenericRepository;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.Services.Materials;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialControllar : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository1<CourseMaterial> materialRepo;
        private readonly IMapper mapper;
        private readonly projectDbContext dbContext;
        private readonly IMaterialServices materialServices;
        protected ApiResponce response;

        public MaterialControllar( IUnitOfWork unitOfWork , IGenericRepository1<CourseMaterial> materialRepo ,IMapper mapper , projectDbContext dbContext , IMaterialServices materialServices )
        {
            this.unitOfWork = unitOfWork;
            this.materialRepo = materialRepo;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.materialServices = materialServices;
            response = new ApiResponce();
       }


        


        [HttpPost("AddTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Instructor")]
        public async Task<IActionResult> AddTask( [FromForm] TaskDTO taskDTO)
        {
            var addTask = await materialServices.AddTask(taskDTO);
            if (addTask.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result="Tha task is added successfully"});

        }


        [HttpPost("AddFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> AddFile([FromForm] FileDTO fileDTO)
        {
            var addfile = await materialServices.AddFile(fileDTO);
            if (addfile.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result = "Tha file is added successfully" });

        }

        [HttpPost("AddAnnouncement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Instructor")]
        public async Task<IActionResult> AddAnnouncement( AnnouncementDTO AnnouncementDTO)
        {

            var addAnnouncement = await materialServices.AddAnnouncement(AnnouncementDTO);
            if (addAnnouncement.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result = "Tha Announcement is added successfully" });

        }


        [HttpPost("AddLink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> AddLink( LinkDTO linkDTO)
        {

            var addLink = await materialServices.AddLink(linkDTO);
            if (addLink.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result = "Tha link is added successfully" });

        }




        [HttpPut("EditTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="InstructorwhoGiveTheMaterial")]
        
        public async Task<IActionResult> EditTask(Guid id, [FromForm] TaskForEditDTO taskDTO)
        {
            await materialServices.deleteFiles(id);
            var editedTask = await materialServices.EditTask(id, taskDTO);
            if (editedTask.IsError) return NotFound(new ApiResponce { ErrorMassages=editedTask.FirstError.Description});
            return Ok(new ApiResponce { Result = "The task is updated successfully" });
        }




        [HttpPut("EditFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> EditFile(Guid id, [FromForm] FileToEditDTO fileDTO)
        {
            await materialServices.deleteFiles(id);
            var editedFile = await materialServices.EditFile(id, fileDTO);
            if (editedFile.IsError) return NotFound(new ApiResponce { ErrorMassages = editedFile.FirstError.Description });
            return Ok(new ApiResponce { Result = "The file is updated successfully" });
        }




        [HttpPut("EditAnnouncement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> EditAnnouncement(Guid id, AnnouncementForEditDTO AnnouncementDTO)
        {
            var editedAnnouncement = await materialServices.EditAnnouncement(id, AnnouncementDTO);
            if (editedAnnouncement.IsError) return NotFound(new ApiResponce { ErrorMassages = editedAnnouncement.FirstError.Description });
            return Ok(new ApiResponce { Result = "The Announcement is updated successfully" });
        }




        [HttpPut("EditLink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> EditLink(Guid id,  LinkForEditDTO linkDTO)
        {
            var editedLink = await materialServices.EDitLink(id, linkDTO);
            if (editedLink.IsError) return NotFound(new ApiResponce { ErrorMassages = editedLink.FirstError.Description });
            return Ok(new ApiResponce { Result = "The link is updated successfully" });
        }



        [HttpDelete("DeleteMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            var materail = await materialServices.DeleteMaterial(id);
            if (materail.IsError) return NotFound(new ApiResponce { ErrorMassages= materail.FirstError.Description} );
            return Ok(new ApiResponce { Result="The material is deleted successfully"});      
        }


        [HttpGet("GetMaterialById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "MaterialInEnrolledCourse")]
        public async Task<IActionResult> GetMaterialByIdAsync(Guid id)
        {
            var material = await materialServices.GetMaterialById(id);
            if (material.IsError) return NotFound(material.FirstError.Description);
            return Ok(new ApiResponce { Result = material.Value });
        }


        [HttpGet("GetAllMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "EnrolledInCourse")]
        public async Task<IActionResult> GetAllMaterialInTheCourseToStudentAsync( Guid? CourseId  , Guid? ConsultationId )
        {
            var AllMaterials = await materialServices.GetAllMaterialInTheCourse(CourseId , ConsultationId );
            if (AllMaterials.IsError) return NotFound(new ApiResponce { ErrorMassages=AllMaterials.FirstError.Description});
            return Ok(new ApiResponce { Result = AllMaterials.Value });
        }

        [HttpPatch("HideOrShowMaterials")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> HideOrShowMaterials(Guid Id, bool isHidden)
        {
            var statusMaterial = await materialServices.changeMaterialStatus(Id, isHidden);
            if (statusMaterial.IsError) return NotFound(new ApiResponce { ErrorMassages=statusMaterial.FirstError.Description});
            return Ok(new ApiResponce { Result="The status of material is changed successfully"});
        }


        

    }
}
