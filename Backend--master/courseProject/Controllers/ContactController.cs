using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using courseProject.Core.IGenericRepository;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Services.Courses;
using courseProject.Services.Skill;
using courseProject.Services.ContactUs;
using courseProject.Core.Models.DTO.ContactUsDTO;
using courseProject.Repository.GenericRepository;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactServices contactServices;
      
        public ContactController(  IContactServices contactServices )
        {
            
            this.contactServices = contactServices;

        }


        [HttpPost("MessageToContactUs")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> AddAMessageToContactUs([FromForm] CreateMessageContactDTO contact)
        {
            await contactServices.AddNewCOntactMessage(contact);
            return Ok("The message is send successfully");
        }


        [HttpPost("GetAllContact")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        
        public async Task<IActionResult> GetAllContact(int? pageNumber , int? pageSize)
        {
            var allContact = await contactServices.GetAllMessages();
            return Ok(Pagination<Contact>.CreateAsync(allContact ,pageNumber , pageSize ).Result);
        }

        [HttpPost("GetContactById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        
        public async Task<IActionResult> GetContactById(Guid Contactid)
        {
            return Ok(await contactServices.getContactById(Contactid));
        }



    }
}
