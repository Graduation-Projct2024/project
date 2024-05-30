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

        

        public async Task<Course> GetCourseByIdAsync(Guid? id)
        {
           return  await dbContext.courses.Include(x=>x.Instructor).ThenInclude(x=>x.user)
                .Include(x=>x.SubAdmin).ThenInclude(x=>x.user)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetNumberOfStudentsInTHeCourseAsync(Guid courseId)
        {
           return   dbContext.studentCourses.Where(x => x.courseId == courseId).Count();
        }

        public Task EditCourseAfterAccreditAsync(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStudentCourse(StudentCourse studentCourse)
        {
             dbContext.Set<StudentCourse>().Update(studentCourse);
        }

        public async Task<IReadOnlyList<Course>> GetAllUndefinedCoursesBySubAdminIdAsync(Guid subAdminId)
        {
            return await dbContext.courses.Include(x=>x.Instructor).ThenInclude(x=>x.user)
                                          .Include(x=>x.SubAdmin).ThenInclude(x=>x.user)
                .Where(x=>x.SubAdminId == subAdminId && x.status.ToLower()=="undefined").ToListAsync();
        }

        public async Task<Course> getAccreditCourseByIdAcync(Guid courseId)
        {
            return await dbContext.courses.Where(x => x.status.ToLower() == "accredit").FirstOrDefaultAsync(x => x.Id == courseId);
        }
    }
}
