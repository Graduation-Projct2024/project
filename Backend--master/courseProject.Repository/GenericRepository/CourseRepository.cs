using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class CourseRepository : GenericRepository1<Course>, ICourseRepository<Course>
    {
        private readonly projectDbContext dbContext;

        public CourseRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

      

        public async Task<Course> GetCourseByIdAsync(int id)
        {
           return  await dbContext.courses.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
