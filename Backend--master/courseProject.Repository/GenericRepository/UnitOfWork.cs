using Microsoft.Extensions.Configuration;
using courseProject.Core.IGenericRepository;
using courseProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {

       
    
        private readonly projectDbContext dbContext;
        private IConfiguration configuration;

        public UnitOfWork(projectDbContext dbContext , IConfiguration configuration)
        {
            this.dbContext = dbContext;
            SubAdminRepository = new SubAdminRepository(dbContext);
            UserRepository = new UserRepository(dbContext, configuration);
            instructorRepositpry = new instructorRepositpry(dbContext);
        }

        public ISubAdminRepository SubAdminRepository { get; set; }
        public IUserRepository UserRepository { get; set ; }
        public IinstructorRepositpry instructorRepositpry { get ; set ; }
    }
}

