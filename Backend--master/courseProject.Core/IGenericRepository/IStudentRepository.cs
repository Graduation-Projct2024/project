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
        public  Task AddInStudentConsulationAsync(StudentConsultations consultation);
        public Task<IReadOnlyList<StudentConsultations>> GetAllConsultations();
        public Task<List<StudentConsultations>> GetAllStudentsInPublicConsulations(int consultationId);
        //   public Task<List<StudentConsultations>> GetStudentInPrivateConsulations(int consultationId);
        public Task<Consultation> GetConsultationById(int consultationId);
        public Task<IReadOnlyList<StudentConsultations>> GetAllOtherPrivateConsultationsAsync(int studentId);
        public Task<IReadOnlyList<StudentConsultations>> GetAllBookedPrivateConsultationsAsync(int studentId);
        public Task addFeedbackAsync(Feedback feedback);
        public Task <IReadOnlyList<Feedback>> GetAllFeedbacksAsync();
        public Task<IReadOnlyList<Feedback>> GetFeedbacksByTypeAsync(string type);
        public Task<Feedback> GetFeedbackByIdAsync(int id);
        public Task<Student> getStudentByIdAsync(int id);
        public Task<IReadOnlyList<StudentConsultations>> GetAllLectureByStudentIdAsync(int StudentId);
        public Task<IReadOnlyList<Consultation>> GetAllPublicConsultationsAsync();
        public Task<IReadOnlyList<Course>> GetAllCoursesAsync(int studentId);
        public Task<StudentCourse> GetFromStudentCourse(int courseId, int studentId);
        public Task RemoveTheRejectedRequestToJoinCourse(StudentCourse studentCourse);
        //  public Task RequestToCreateCustomCourseAsync ()
    }
}
