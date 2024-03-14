using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IStudentRepository : IGenericRepository1<Student>
    {

        public Task CreateStudentAccountAsync(Student student);
        public Task EnrollCourse(StudentCourse studentCourse);   

    }
}
