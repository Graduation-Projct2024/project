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
           return  await dbContext.courses.Include(x=>x.Instructor).ThenInclude(x=>x.user)
                .Include(x=>x.SubAdmin).ThenInclude(x=>x.user)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetNumberOfStudentsInTHeCourseAsync(int courseId)
        {
           return   dbContext.studentCourses.Where(x => x.courseId == courseId).Count();
        }

        public Task EditCourseAfterAccreditAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStudentCourse(StudentCourse studentCourse)
        {
             dbContext.Set<StudentCourse>().Update(studentCourse);
        }

        public async Task<IReadOnlyList<Course>> GetAllUndefinedCoursesBySubAdminIdAsync(int subAdminId)
        {
            return await dbContext.courses.Include(x=>x.Instructor).ThenInclude(x=>x.user)
                                          .Include(x=>x.SubAdmin).ThenInclude(x=>x.user)
                .Where(x=>x.SubAdminId == subAdminId && x.status.ToLower()=="undefined").ToListAsync();
        }
    }
}
