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
        public Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(int Studentid);
        public Task SubmitTaskAsync(Student_Task_Submissions student_Task);
        public Task<IReadOnlyList<Student>> GetAllStudentsInTheSameCourseAsync(int courseId);
        public Task BookLectureAsync(Consultation consultation);
       
        public Task<Student> getStudentByIdAsync(int id);
        //  public Task RequestToCreateCustomCourseAsync ()
    }
}
