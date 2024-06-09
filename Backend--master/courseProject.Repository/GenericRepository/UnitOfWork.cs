﻿using Microsoft.Extensions.Configuration;
using courseProject.Core.IGenericRepository;
using courseProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.Core.Models;
using Microsoft.AspNetCore.Http;


namespace courseProject.Repository.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {

       
    
        private readonly projectDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
      
        private IConfiguration configuration;

        public UnitOfWork(projectDbContext dbContext , IConfiguration configuration , IHttpContextAccessor httpContextAccessor )
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
           
            SubAdminRepository = new SubAdminRepository(dbContext);
            UserRepository = new UserRepository(dbContext, configuration, httpContextAccessor);
            instructorRepositpry = new instructorRepositpry(dbContext);
            StudentRepository=new StudentRepository(dbContext);
            AdminRepository = new AdminRepository(dbContext);
            FileRepository = new FileRepository(httpContextAccessor);
            CourseRepository = new CourseRepository(dbContext);
            materialRepository = new MaterialRepository(dbContext);
            eventRepository = new EventRepository(dbContext);
            FeedbackRepository = new FeedbackRepository(dbContext);
            lecturesRepository = new LecturesRepository(dbContext);
            RequestRepository = new RequestRepository(dbContext);
            skillRepository= new SkillRepository(dbContext);
            studentCourseRepository = new StudentCourseRepository(dbContext);
            submissionRepository = new SubmissionRepository(dbContext);
            contactRepository = new ContactRepository(dbContext);
     //       EmailService = new SmtpEmailService(configuration);

        }

        public ISubAdminRepository SubAdminRepository { get; set; }
        public IUserRepository UserRepository { get; set ; }
        public IinstructorRepositpry instructorRepositpry { get ; set ; }
        public IStudentRepository StudentRepository { get; set ; }
        public IAdminRepository AdminRepository { get; set ; }
        public IFileRepository FileRepository { get; set; }
        public ICourseRepository<Course> CourseRepository { get; set ; }
        public IMaterialRepository materialRepository { get; set ; }
        public IEventRepository eventRepository { get; set ; }
        public IEmailService EmailService { get; set; }
        public IFeedbackRepository FeedbackRepository { get; set ; }
        public ILecturesRepository lecturesRepository { get; set ; }
        public IRequestRepository RequestRepository { get; set ; }
        public ISkillRepository skillRepository { get; set ; }
        public IStudentCourseRepository studentCourseRepository { get; set ; }
        public ISubmissionRepository submissionRepository { get; set ; }
        public IContactRepository contactRepository { get; set ; }
        //  Core.IGenericRepository.ICourseRepository<Course> IUnitOfWork.CourseRepository { get ; set; }
    }
}

