using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class CourseRepository : GenericRepository1<Course>, ICourseRepository
    {
        public CourseRepository(projectDbContext dbContext) : base(dbContext)
        {
        }

        public Task GetAllCoursesAsync(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
