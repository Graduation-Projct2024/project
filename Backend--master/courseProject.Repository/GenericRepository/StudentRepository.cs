using courseProject.core.Models;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace courseProject.Repository.GenericRepository
{
    public class StudentRepository : GenericRepository1<Student>, IStudentRepository
    {
        private readonly projectDbContext dbContext;

        public StudentRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            
        }

        

        public async Task AddInStudentConsulationAsync(StudentConsultations consultation)
        {
            await dbContext.Set<StudentConsultations>().AddAsync(consultation);
        }

        public async Task CreateStudentAccountAsync(Student student)
        {
          await  dbContext.Set<Student>().AddAsync(student);
        }

       

        public async Task EnrollCourse(StudentCourse studentCourse)
        {
           await dbContext.Set<StudentCourse>().AddAsync(studentCourse);
        }

       

        public async Task<IReadOnlyList<Student>> GetAllStudentsInTheSameCourseAsync(Guid courseId)
        {
            return await dbContext.students.Include(x=>x.user)
                          .Where(student => student.studentCourses.Any(sc => sc.courseId == courseId && sc.status.ToLower()== "joind"))
                          .ToListAsync();
        }

       

       

       

        public async Task<Student> getStudentByIdAsync(Guid? id )
        {          
            return await dbContext.students.FirstOrDefaultAsync(x => x.StudentId == id);
        }

       
        public async Task<List<StudentConsultations>> GetAllStudentsInPublicConsulations(Guid consultationId)
        {
            return await dbContext.StudentConsultations.Where(x=>x.consultationId== consultationId)
                .Where(x => x.consultation.type.ToLower() == "public")
                .Include(x=>x.Student).ThenInclude(x=>x.user).ToListAsync();
        }

       

      

        public async Task<StudentCourse> GetFromStudentCourse(Guid courseId, Guid studentId)
        {
           return await dbContext.studentCourses.FirstOrDefaultAsync(x=>x.courseId==courseId &&  x.StudentId==studentId);
        }

        public async Task RemoveTheRejectedRequestToJoinCourse(StudentCourse studentCourse)
        {
             dbContext.studentCourses.Remove(studentCourse);
        }

       
    }
}
