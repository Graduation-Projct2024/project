using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using System.Net;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialControllar : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        protected ApiResponce response;

        public MaterialControllar( IUnitOfWork unitOfWork ,IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            response = new ApiResponce();
       }


        [HttpPost("AddMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<ApiResponce>> AddMaterial (CourseMaterialDTO courseMaterialDTO)
        {
            var materialMapped = mapper.Map<CourseMaterial>(courseMaterialDTO);
            var materialAdded= unitOfWork.instructorRepositpry.AddMaterial(materialMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            if (success > 0)
            {
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Result= courseMaterialDTO;
                return Ok(response);

            }
            response.StatusCode=HttpStatusCode.BadRequest;
            response.IsSuccess=false;
            response.ErrorMassages.Add("This Material is not added");
            return BadRequest(response);
        }



        [HttpDelete("DeleteMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponce>> DeleteMaterial(int id)
        {
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

    }
}
