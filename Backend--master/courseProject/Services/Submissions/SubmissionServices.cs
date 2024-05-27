﻿using AutoMapper;
using courseProject.core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using ErrorOr;
using System.Net;
using courseProject.Core.IGenericRepository;
using courseProject.ServiceErrors;
using courseProject.Core.Models.DTO.MaterialsDTO;

namespace courseProject.Services.Submissions
{
    public class SubmissionServices : ISubmissionServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SubmissionServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        

        public async Task<ErrorOr<IReadOnlyList<StudentSubmissionDTO>>> GetAllSubmissionForTask(Guid taskId)
        {
            var taskFound = await unitOfWork.materialRepository.GetMaterialByIdAsync(taskId);
            if (taskFound == null || taskFound.type.ToLower() != "task") return ErrorMaterial.NotFound;
            
            
            var GetSubmissions = await unitOfWork.instructorRepositpry.GetAllSubmissionsByTaskIdAsync(taskId);
           
            var submissionMapper = mapper.Map<IReadOnlyList<Student_Task_Submissions>, IReadOnlyList<StudentSubmissionDTO>>(GetSubmissions);

            return submissionMapper.ToErrorOr();
        }

        public async Task<ErrorOr<Created>> AddTaskSubmission(Guid Studentid, Guid taskid, SubmissionsDTO submissions)
        {
            var taskFound = await unitOfWork.materialRepository.GetMaterialByIdAsync(taskid);
            if(taskFound==null) return ErrorMaterial.NotFound;
            var studentFound = await unitOfWork.UserRepository.ViewProfileAsync(Studentid, "student");
            if (studentFound == null ) return ErrorStudent.NotFound;
            
            
            var student_Task = mapper.Map<SubmissionsDTO, Student_Task_Submissions>(submissions);
            student_Task.StudentId = Studentid;
            student_Task.TaskId = taskid;
            if (student_Task.pdf != null)
            {
                student_Task.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(student_Task.pdf);
            }
            await unitOfWork.StudentRepository.SubmitTaskAsync(student_Task);
             await unitOfWork.StudentRepository.saveAsync();
            if (submissions.pdf != null)
            {
                student_Task.pdfUrl = "http://localhost:5134/" + student_Task.pdfUrl;
            }
            return Result.Created;
        }
    }
}
