﻿using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using System.Net;

namespace courseProject.Services.Instructors
{
    public class instructorServices : IinstructorServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        

        public instructorServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
         
        }

       

        public async Task<IReadOnlyList<Instructor>> GetAllInstructors()
        {
            return await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();
        }

        public async Task<ErrorOr<Instructor>> getInstructorById(Guid InstructorId)
        {
            var instructorFound = await unitOfWork.UserRepository.ViewProfileAsync(InstructorId, "instructor");
            if (instructorFound == null) return ErrorInstructor.NotFound;
            return await unitOfWork.instructorRepositpry.getInstructorByIdAsync(InstructorId);
        }

        public async Task<ErrorOr<Created>> AddOfficeHours(Guid InstructorId, WorkingHourDTO _Working_Hours)
        {
           
            var instructorFound = await unitOfWork.instructorRepositpry.GetEmployeeById(InstructorId);
            if (instructorFound == null) return ErrorInstructor.NotFound;

            if (!CommonClass.IsValidTimeFormat(_Working_Hours.startTime) || !CommonClass.IsValidTimeFormat(_Working_Hours.endTime))
                return ErrorInstructor.InvalidTime;
            var OfficeHourMapper = mapper.Map<WorkingHourDTO, Instructor_Working_Hours>(_Working_Hours);
            if (!CommonClass.CheckStartAndEndTime(OfficeHourMapper.startTime, OfficeHourMapper.endTime))
                return ErrorInstructor.InvalidTime;
            OfficeHourMapper.InstructorId = InstructorId;
            await unitOfWork.instructorRepositpry.AddOfficeHoursAsync(OfficeHourMapper);
            await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<IReadOnlyList<GetWorkingHourDTO>>> GetInstructorOfficeHours(Guid InstructorId)
        {
            var instructorFound = await unitOfWork.instructorRepositpry.GetEmployeeById(InstructorId);
            if (instructorFound == null)
                return ErrorInstructor.NotFound;
            var InstructorOfficeHours = await unitOfWork.instructorRepositpry.GetOfficeHourByIdAsync(InstructorId);
            var InstructorOfficeHoursMapper = mapper.Map<IReadOnlyList<Instructor_Working_Hours>, IReadOnlyList<GetWorkingHourDTO>>(InstructorOfficeHours);
            return InstructorOfficeHoursMapper.ToErrorOr();
        }

        public async Task<IReadOnlyList<EmployeeListDTO>> GetAllInstructorsList()
        {
            var GetInstructors = await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();
           
            var CustomCoursesMapper = mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<EmployeeListDTO>>(GetInstructors);
            return CustomCoursesMapper;
        }

        public async Task<IReadOnlyList<Instructor_OfficeHoursDTO>> GetAllInstructorsOfficeHours()
        {
            var AllOfficeHours = await unitOfWork.instructorRepositpry.getAllInstructorsOfficeHoursAsync();
           
            var InstrctorOfficeHoursMapper = mapper.Map<IReadOnlyList<Instructor_Working_Hours>, IReadOnlyList<Instructor_OfficeHoursDTO>>(AllOfficeHours);
            return InstrctorOfficeHoursMapper;
        }

        public async Task<ErrorOr< IReadOnlyList<EmployeeListDTO>>> GetListOfInstructorForLectures(Guid skillId, string startTime, string endTime, DateTime date)
        {
            if (!CommonClass.IsValidTimeFormat(startTime) || !CommonClass.IsValidTimeFormat(endTime))
                return ErrorLectures.InvalidTime;
            TimeSpan StartTime = CommonClass.ConvertToTimeSpan(startTime);
            TimeSpan EndTime = CommonClass.ConvertToTimeSpan(endTime);
            if (StartTime >= EndTime) return ErrorLectures.GraterTime;

            if ((EndTime - StartTime) > TimeSpan.Parse("02:00") || (EndTime - StartTime) < TimeSpan.Parse("00:30"))
                return ErrorLectures.limitationTime;
            var getAllSkills = await unitOfWork.AdminRepository.GetAllSkillsAsync();
            
            var getInstructors = await unitOfWork.instructorRepositpry.getAListOfInstructorDependOnSkillsAndOfficeTime(skillId, StartTime, EndTime, date);           
            var instructorMapper = mapper.Map<IReadOnlyList<Instructor_Working_Hours>, IReadOnlyList<EmployeeListDTO>>(getInstructors);            
            return instructorMapper.ToErrorOr();
            
        }

        public async Task<ErrorOr<Updated>> AddSkillDescription(Guid instructorId, SkillDescriptionDTO skillDescriptionDTO)
        {
            var instructor = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(instructorId);
            if (instructor == null) return ErrorInstructor.NotFound;

            instructor.skillDescription = skillDescriptionDTO.skillDescription;
            await unitOfWork.instructorRepositpry.updateSubAdminAsync(instructor);
            await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }
    }
}
