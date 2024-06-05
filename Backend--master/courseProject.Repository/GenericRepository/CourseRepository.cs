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

        public async Task<IReadOnlyList<Course>> GetAllCoursesGivenByInstructorIdAsync(Guid Instructorid)
        {
            return await dbContext.courses.Where(x => x.InstructorId == Instructorid && (x.status.ToLower() == "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finished")).ToListAsync();
        }

        public async Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(Guid Studentid)
        {
            return await dbContext.studentCourses.Include(x => x.Course).Where(x => x.StudentId == Studentid && x.status.ToLower() == "joind").ToListAsync();
        }



        public async Task<IReadOnlyList<Course>> GetAllCoursesAsync(Guid studentId)
        {
            var allCourses = await dbContext.courses
                .Where(x => x.status.ToLower() == "accredit")
                .Include(x => x.studentCourses)
                .Include(x => x.Instructor).ThenInclude(x => x.user)
                .Include(x => x.SubAdmin).ThenInclude(x => x.user)
                .ToListAsync();

            if (allCourses.Count > 0)
            {

                StudentCourse studentCourse;
                foreach (var course in allCourses)
                {
                    studentCourse = course.studentCourses.FirstOrDefault(x => x.StudentId == studentId && course.Id == x.courseId);

                    if (studentCourse != null && course.studentCourses.Any(x => x.courseId == course.Id && x.StudentId == studentId && (x.Course.Deadline >= DateTime.Today.Date || x.Course.Deadline == null)))
                    {

                        studentCourse.isEnrolled = true;
                    }
                    //else
                    //{
                    //    studentCourse.isEnrolled = false;
                    //}
                }
            }
            return allCourses;
        }


        public async Task CreateCourse(Course model)
        {
            await dbContext.Set<Course>().AddAsync(model);

        }
        public async Task updateCourse(Course course)
        {
            dbContext.Entry(course).State = EntityState.Modified;
        }



    }
}
