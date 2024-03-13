using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using courseProject.Repository.GenericRepository;
using System.Net;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsContraller : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository1<Student> studentRepo;
        private readonly IMapper mapper;
        protected ApiResponce response;

        public StudentsContraller(IUnitOfWork unitOfWork, IGenericRepository1<Student> StudentRepo , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            studentRepo = StudentRepo;
            this.mapper = mapper;
            response=new ApiResponce();
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


        ////[HttpPost("CreateStudent")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult<Student>> CreateStudent(RegistrationRequestDTO model)
        //{

        //    var modelMapped = mapper.Map<Student>(model);
        //    using (var transaction = await unitOfWork.SubAdminRepository.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //           await unitOfWork.UserRepository.RegisterAsync(model);
        //            var success1 = await unitOfWork.StudentRepository.saveAsync();

        //            await unitOfWork.StudentRepository.CreateStudentAccountAsync(modelMapped);
        //            var success2 = await unitOfWork.StudentRepository.saveAsync();


        //            if (success1 > 0 && success2 > 0)
        //            { 
        //                await transaction.CommitAsync();
        //                response.StatusCode = (HttpStatusCode)StatusCodes.Status201Created;
        //                response.IsSuccess = true;
        //                response.Result = model;
        //                return Ok(response);
        //            }

        //            return BadRequest(response);
        //        }
        //        catch (Exception ex)
        //        {
        //            await transaction.RollbackAsync();


        //            response.StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest;
        //            response.IsSuccess = false;

        //            return BadRequest(response);
        //        }

        //    }
        }


    }

