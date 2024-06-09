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
                .Where(x=>x.SubAdminId == subAdminId && x.status.ToLower()=="undefined").ToListAsync();
        }

        public async Task<Course> getAccreditCourseByIdAcync(Guid courseId)
        {
            return await dbContext.courses.Where(x => x.status.ToLower() == "accredit").FirstOrDefaultAsync(x => x.Id == courseId);
        }

        public async Task<IReadOnlyList<Course>> GetAllCoursesGivenByInstructorIdAsync(Guid Instructorid)
        {
            return await dbContext.courses.Where(x => x.InstructorId == Instructorid && (x.status.ToLower() == "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finish")).ToListAsync();
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



        // Retrieve all courses with specific statuses : accredit , start or finish (A_S_F)
        public async Task<IReadOnlyList<Course>> GetAllCoursesAsync()
        {
           

                return await dbContext.courses
                    .Where(x => x.status.ToLower() == "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finish")
                    .Include(x => x.Instructor.user).Include(x => x.SubAdmin.user).ToListAsync();
           
            //// Apply filtering and sorting using Sieve without pagination
            //var filteredAndSortedCourses = sieveProcessor.Apply(sieveModel, courses, applyPagination: false);

            //// Get the total count after filtering and sorting but before pagination
            //var totalCount = await filteredAndSortedCourses.CountAsync();

            //// Apply pagination
            //var pagedCourses = await sieveProcessor.Apply(sieveModel, filteredAndSortedCourses).ToListAsync();


            // Create and return the PagedResponse
       //    return PaginationClass<Course>.CreateAsync(pagedCourses,totalCount, sieveModel.Page , sieveModel.PageSize ).Result;
          //  return await sieveProcessor.Apply(sieveModel, courses).ToListAsync();
           
        }

    }
}
