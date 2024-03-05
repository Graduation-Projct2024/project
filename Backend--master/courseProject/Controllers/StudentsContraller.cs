using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsContraller : ControllerBase
    {
        private readonly IGenericRepository1<Student> studentRepo;
        private readonly IMapper mapper;

        public StudentsContraller( IGenericRepository1<Student> StudentRepo , IMapper mapper)
        {
            studentRepo = StudentRepo;
            this.mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task <ActionResult<IEnumerable<Student>>> GetAllStudentsAsync()
        {
            var Students = await studentRepo.GetAllStudentsAsync();

            if(Students == null)
            {
                return NotFound();
            }
            
            var mappedStudentDTO = mapper.Map<IEnumerable<Student>, IEnumerable<StudentsInformationDto>>(Students);

            return Ok(mappedStudentDTO);
        }


        [HttpGet("contact")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IReadOnlyList<Student>>> GetAllStudentsForContactAsync()
        {
            var students = await studentRepo.GetAllStudentsForContactAsync();
            if (students == null)
            {
                return NotFound();
            }
            var mapperStudents = mapper.Map<IReadOnlyList<Student>, IReadOnlyList<ContactDto>>(students);
            return Ok(mapperStudents);

        }


        }
}
