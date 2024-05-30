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
        public Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(Guid Studentid);
        public Task SubmitTaskAsync(Student_Task_Submissions student_Task);
        public Task<IReadOnlyList<Student>> GetAllStudentsInTheSameCourseAsync(Guid courseId);
        public Task BookLectureAsync(Consultation consultation);
        public  Task AddInStudentConsulationAsync(StudentConsultations consultation);
        public Task<IReadOnlyList<StudentConsultations>> GetAllConsultations();
        public Task<List<StudentConsultations>> GetAllStudentsInPublicConsulations(Guid consultationId);
        //   public Task<List<StudentConsultations>> GetStudentInPrivateConsulations(int consultationId);
        public Task<Consultation> GetConsultationById(Guid? consultationId);
        public Task<IReadOnlyList<StudentConsultations>> GetAllOtherPrivateConsultationsAsync(Guid studentId);
        public Task<IReadOnlyList<StudentConsultations>> GetAllBookedPrivateConsultationsAsync(Guid studentId);
        public Task addFeedbackAsync(Feedback feedback);
        public Task <IReadOnlyList<Feedback>> GetAllFeedbacksAsync();
        public Task<IReadOnlyList<Feedback>> GetFeedbacksByTypeAsync(string type);
        public Task<Feedback> GetFeedbackByIdAsync(Guid id);
        public Task<Student> getStudentByIdAsync(Guid? id);
        public Task<IReadOnlyList<StudentConsultations>> GetAllLectureByStudentIdAsync(Guid StudentId);
        public Task<IReadOnlyList<Consultation>> GetAllPublicConsultationsAsync();
        public Task<IReadOnlyList<Course>> GetAllCoursesAsync(Guid studentId);
        public Task<StudentCourse> GetFromStudentCourse(Guid courseId, Guid studentId);
        public Task RemoveTheRejectedRequestToJoinCourse(StudentCourse studentCourse);



        public Task searchStudent(string query);
        //  public Task RequestToCreateCustomCourseAsync ()
    }
}
