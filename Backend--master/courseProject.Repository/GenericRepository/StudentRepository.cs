using courseProject.core.Models;
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
    public class StudentRepository : GenericRepository1<Student>, IStudentRepository
    {
        private readonly projectDbContext dbContext;

        public StudentRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            
        }

        public async Task BookLectureAsync(Consultation consultation)
        {
           await dbContext.Set<Consultation>().AddAsync(consultation);
        }

        public async Task CreateStudentAccountAsync(Student student)
        {
          await  dbContext.Set<Student>().AddAsync(student);
        }

       

        public async Task EnrollCourse(StudentCourse studentCourse)
        {
           await dbContext.Set<StudentCourse>().AddAsync(studentCourse);
        }

        public async Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(int Studentid)
        {
         return await  dbContext.studentCourses.Include(x=>x.Course).Where(x => x.StudentId == Studentid).ToListAsync();
        }

        public async Task<IReadOnlyList<Student>> GetAllStudentsInTheSameCourseAsync(int courseId)
        {
            return await dbContext.students.Include(x=>x.user)
                          .Where(student => student.studentCourses.Any(sc => sc.courseId == courseId))
                          .ToListAsync();
        }

        //public async Task GetSubmissionById(int taskId)
        //{
        //    await dbContext.Student_Task_Submissions.Where(x=>x.TaskId==taskId).ToListAsync();
        //}

        public async Task SubmitTaskAsync(Student_Task_Submissions student_Task)
        {
            await dbContext.Set<Student_Task_Submissions>().AddAsync(student_Task);
        }
    }
}
