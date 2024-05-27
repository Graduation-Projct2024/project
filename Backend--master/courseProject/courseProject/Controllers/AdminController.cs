using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using courseProject.Core.IGenericRepository;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Services.Courses;
using courseProject.Services.Skill;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICourseServices courseServices;
        private readonly ISkillsServices skillsServices;
        private readonly ApiResponce responce;

        public AdminController(IUnitOfWork unitOfWork , IMapper mapper , ICourseServices courseServices , ISkillsServices skillsServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.courseServices = courseServices;
            this.skillsServices = skillsServices;
            responce =new ApiResponce();
        }

       


       



    }
}
