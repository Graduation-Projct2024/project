﻿using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IUnitOfWork
    {

        public ISubAdminRepository SubAdminRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IinstructorRepositpry instructorRepositpry { get; set; }
        public IStudentRepository StudentRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }
        public IFileRepository FileRepository { get; set; }
        public ICourseRepository<Course> CourseRepository { get; set; }
        public IMaterialRepository materialRepository { get; set; }
    }
}
