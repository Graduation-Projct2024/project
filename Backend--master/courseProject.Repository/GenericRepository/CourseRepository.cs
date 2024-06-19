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
    

        public CourseRepository(projectDbContext dbContext ) : base(dbContext)
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
                .Where(x=>x.SubAdminId == subAdminId && x.status.ToLower()=="undefined")
                .OrderByDescending(x=>x.dateOfAdded)
                .ToListAsync();
        }

        public async Task<Course> getAccreditCourseByIdAcync(Guid courseId)
        {
            return await dbContext.courses.Where(x => x.status.ToLower() != "undefined" || x.status.ToLower() != "reject").FirstOrDefaultAsync(x => x.Id == courseId);
        }

        public async Task<IReadOnlyList<Course>> GetAllCoursesGivenByInstructorIdAsync(Guid Instructorid)
        {
            return await dbContext.courses.Where(x => x.InstructorId == Instructorid && (x.status.ToLower() == "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finish"))
                .OrderByDescending(x => x.dateOfAdded)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(Guid Studentid)
        {
            return await dbContext.studentCourses.Include(x => x.Course).Where(x => x.StudentId == Studentid && x.status.ToLower() == "joind")
                .OrderByDescending(x=>x.EnrollDate)
                .ToListAsync();
        }



        public async Task<IReadOnlyList<Course>> GetAllCoursesAsync(Guid studentId)
        {
            var allCourses = await dbContext.courses
                .Where(x => x.status.ToLower() != "undefined" || x.status.ToLower()!="reject")
                
                .Include(x => x.studentCourses)
                .Include(x => x.Instructor).ThenInclude(x => x.user)
                .Include(x => x.SubAdmin).ThenInclude(x => x.user)
                .OrderByDescending(x=>x.dateOfAdded)
                .ToListAsync();

            if (allCourses.Count > 0)
            {

                StudentCourse studentCourse;
                int studentCourseCount;
                foreach (var course in allCourses)
                {
                    studentCourse = course.studentCourses.FirstOrDefault(x => x.StudentId == studentId && course.Id == x.courseId);
                    studentCourseCount = course.studentCourses.Count();

                    if (studentCourse != null &&( course.studentCourses.Any(x => x.courseId == course.Id && x.StudentId == studentId) ))/*&& (x.Course.Deadline >= DateTime.Today.Date || x.Course.Deadline == null)))*/
                    {

                        studentCourse.isEnrolled = true;
                    }

                    else
                    {
                       
                        if (((course.Deadline.Value < DateTime.Now.Date) ||course.limitNumberOfStudnet <= studentCourseCount) && course.status.ToLower()!="finish")
                        {

                            course.isAvailable = false;
                        }
                        else
                        {
                            course.isAvailable = true;
                        }
                    }
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



        // Retrieve all courses with specific statuses : accredit , start or finish (A_S_F)
        public async Task<IReadOnlyList<Course>> GetAllCoursesAsync()
        {
           

                return await dbContext.courses
                    .Where(x => x.status.ToLower() == "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finish")
                    .Include(x => x.Instructor.user).Include(x => x.SubAdmin.user).ToListAsync();
           
           
           
        }

    }
}
