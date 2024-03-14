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
    public class StudentRepository : GenericRepository1<Student>, IStudentRepository
    {
        private readonly projectDbContext dbContext;

        public StudentRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            
        }

        public async Task CreateStudentAccountAsync(Student student)
        {
          await  dbContext.Set<Student>().AddAsync(student);
        }

       

        public async Task EnrollCourse(StudentCourse studentCourse)
        {
           await dbContext.Set<StudentCourse>().AddAsync(studentCourse);
        }
    }
}
