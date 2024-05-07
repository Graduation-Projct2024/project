using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using System.Net;
using Microsoft.EntityFrameworkCore;
using courseProject.Repository.Data;
using System.Linq;
using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;

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
        protected ApiResponce response;

        public MaterialControllar( IUnitOfWork unitOfWork , IGenericRepository1<CourseMaterial> materialRepo ,IMapper mapper , projectDbContext dbContext )
        {
            this.unitOfWork = unitOfWork;
            this.materialRepo = materialRepo;
            this.mapper = mapper;
            this.dbContext = dbContext;
            response = new ApiResponce();
       }


        


        [HttpPost("AddTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddTask( [FromForm] TaskDTO taskDTO)
        {
            await unitOfWork.FileRepository.UploadFile1(taskDTO.pdf);
            var taskMapped = mapper.Map<TaskDTO, CourseMaterial>(taskDTO);
            taskMapped.type = "Task";
            taskMapped.courseId = taskDTO.courseId;
            taskMapped.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(taskDTO.pdf);
            await unitOfWork.instructorRepositpry.AddMaterial(taskMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Result = taskDTO;
                return Ok(response);

            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMassages.Add("This Task is not added");
            return BadRequest(response);

        }


        [HttpPost("AddFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddFile([FromForm] FileDTO fileDTO)
        {
            await unitOfWork.FileRepository.UploadFile1(fileDTO.pdf);
            var fileMapped = mapper.Map<FileDTO, CourseMaterial>(fileDTO);
            fileMapped.type = "File";
            fileMapped.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(fileDTO.pdf);
            await unitOfWork.instructorRepositpry.AddMaterial(fileMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Result = fileDTO;
                return Ok(response);

            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMassages.Add("This File is not added");
            return BadRequest(response);

        }

        [HttpPost("AddAnnouncement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddAnnouncement( AnnouncementDTO AnnouncementDTO)
        {
           
            var AnnouncementMapped = mapper.Map<AnnouncementDTO, CourseMaterial>(AnnouncementDTO);
            AnnouncementMapped.type = "Announcement";
            await unitOfWork.instructorRepositpry.AddMaterial(AnnouncementMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Result = AnnouncementDTO;
                return Ok(response);

            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMassages.Add("This Announcement is not added");
            return BadRequest(response);

        }


        [HttpPost("AddLink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> AddLink( LinkDTO linkDTO)
        {

            var linkMapped = mapper.Map<LinkDTO, CourseMaterial>(linkDTO);
            linkMapped.type = "Link";
            await unitOfWork.instructorRepositpry.AddMaterial(linkMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Result = linkDTO;
                return Ok(response);

            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMassages.Add("This Link is not added");
            return BadRequest(response);

        }




        [HttpPut("EditTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EditTask(int id, [FromForm] TaskDTO taskDTO)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is equal 0" };
                return BadRequest(response);
            }

            if (taskDTO == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Task is not found" };
                return NotFound(response);
            }
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var TaskToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync( id);
            if (TaskToUpdate == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Task is not found" };
                return NotFound(response);
            }

            var Taskmapper = mapper.Map(taskDTO, TaskToUpdate);
            Taskmapper.Id = id;
            Taskmapper.type = "Task";
            Taskmapper.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(taskDTO.pdf);
            await unitOfWork.instructorRepositpry.EditMaterial(Taskmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            if (success1 > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = Taskmapper;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }




        [HttpPut("EditFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EditFile(int id, [FromForm] FileDTO fileDTO)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is equal 0" };
                return BadRequest(response);
            }

            if (fileDTO == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the File is not found" };
                return NotFound(response);
            }
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var FileToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (FileToUpdate == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the File is not found" };
                return NotFound(response);
            }

            var filemapper = mapper.Map(fileDTO, FileToUpdate );
            filemapper.Id = id;
            filemapper.type = "File";
            filemapper.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(fileDTO.pdf);
            await unitOfWork.instructorRepositpry.EditMaterial(filemapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            if (success1 > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = filemapper;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }




        [HttpPut("EditAnnouncement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EditAnnouncement(int id, AnnouncementDTO AnnouncementDTO)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is equal 0" };
                return BadRequest(response);
            }

            if (AnnouncementDTO == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Announcement is not found" };
                return NotFound(response);
            }
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var AnnouncementToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (AnnouncementToUpdate == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Announcement is not found" };
                return NotFound(response);
            }

            var Announcementmapper = mapper.Map(AnnouncementDTO, AnnouncementToUpdate);
            Announcementmapper.Id = id;
            Announcementmapper.type = "Announcement";
            await unitOfWork.instructorRepositpry.EditMaterial(Announcementmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            if (success1 > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = Announcementmapper;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }




        [HttpPut("EditLink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> EditLink(int id,  LinkDTO linkDTO)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(response);
            }

            if (linkDTO == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Link is not found" };
                return NotFound(response);
            }
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var LinkToUpdate =  await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (LinkToUpdate == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Link is not found" };
                return NotFound(response);
            }

            var Linkmapper = mapper.Map(linkDTO, LinkToUpdate);
            Linkmapper.Id = id;
            Linkmapper.type = "Link";
            await unitOfWork.instructorRepositpry.EditMaterial(Linkmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            if (success1 > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = Linkmapper;
                return Ok(response);
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            return BadRequest(response);
        }



        [HttpDelete("DeleteMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> DeleteMaterial(int id)
        {
            if (id <= 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is less or equal 0" };
                return BadRequest(response);
            }
            var materail = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (materail == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Id is not found" };
                return NotFound(response);
            }
            await unitOfWork.instructorRepositpry.DeleteMaterial(id);
            var success =await unitOfWork.instructorRepositpry.saveAsync();
            if(success > 0)
            {
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;              
                return Ok(response);

            }
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMassages.Add("This Material is not deleted");
            return BadRequest(response);
        
        }


        [HttpGet("GetMaterialById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
       // [Authorize(Policy = "MaterialInEnrolledCourse")]
        public async Task<ActionResult<ApiResponce>> GetMaterialByIdAsync( int id)
        {
            if (id <= 0)
            {
                response.StatusCode=HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMassages = new List<string>() { $"The {id} is less or equal 0 " };
                return BadRequest(response);
            }

            var material =await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (material == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Material is not found" };
                return NotFound(response);
            }
            

            if (material.type.ToLower() == "task")
            {
                var Taskmapper = mapper.Map<CourseMaterial, TaskForRetriveDTO>(material);
                response.Result = Taskmapper;

            }
            else if (material.type.ToLower() == "file")
            {
                var Filemapper = mapper.Map<CourseMaterial, FileForRetriveDTO>(material);
                response.Result = Filemapper;

            }
            else if (material.type.ToLower() == "announcement")
            {
                var Announcementmapper = mapper.Map<CourseMaterial, AnnouncementForRetriveDTO>(material);
                response.Result = Announcementmapper;

            }
            else if (material.type.ToLower() == "link")
            {
                var Linkmapper = mapper.Map<CourseMaterial, LinkForRetriveDTO>(material);
                response.Result = Linkmapper;
            }
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);

        }


        [HttpGet("GetAllMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
       // [Authorize(Policy = "EnrolledInCourse")]
        public async Task<ActionResult<ApiResponce>> GetAllMaterialInTheCourseAsync([FromQuery]int CourseId)
        {
            if (CourseId == 0)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMassages = new List<string>() { "The Id is equal 0" };
                return BadRequest(response);
            }

           var material= await unitOfWork.materialRepository.GetAllMaterialInSameCourse(CourseId);
            
            if(material == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMassages = new List<string>() { "the Material is not found" };
                return NotFound(response);
            }            
            ArrayList arrayList = new ArrayList();

            foreach (var m in material)
            {
                if (m.type.ToLower() == "task")
                {
                    var Taskmapper = mapper.Map<CourseMaterial,TaskForRetriveDTO>(m);
                  //  Taskmapper.pdfUrl = $"http://localhost:5134/{Taskmapper.pdfUrl}";
                    arrayList.Add(Taskmapper);

                }
                else if (m.type.ToLower() == "file")
                {
                    var Filemapper = mapper.Map<CourseMaterial,FileForRetriveDTO>(m);
                   // Filemapper.pdfUrl = $"http://localhost:5134/{Filemapper.pdfUrl}";
                    arrayList.Add(Filemapper);
                   

                }
                else if (m.type.ToLower() == "announcement")
                {
                    var Announcementmapper = mapper.Map<CourseMaterial,AnnouncementForRetriveDTO>(m);

                    arrayList.Add(Announcementmapper);

                }
                else if (m.type.ToLower() == "link")
                {
                    var Linkmapper = mapper.Map<CourseMaterial, LinkForRetriveDTO>(m);
                    arrayList.Add(Linkmapper);

                }
            }

            return Ok(arrayList);

        }





    }
}
