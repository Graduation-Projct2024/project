using courseProject.core.Models;
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
      
       
        public Task<IReadOnlyList<Student>> GetAllStudentsInTheSameCourseAsync(Guid courseId);
      
        public  Task AddInStudentConsulationAsync(StudentConsultations consultation);

        public Task<IReadOnlyList<Student>> GetAllStudentsAsync();
        public Task<List<StudentConsultations>> GetAllStudentsInPublicConsulations(Guid consultationId);
        public Task<Student> getStudentByIdAsync(Guid? id);
      
       
        public Task<StudentCourse> GetFromStudentCourse(Guid courseId, Guid studentId);
        public Task RemoveTheRejectedRequestToJoinCourse(StudentCourse studentCourse);



      
    }
}
