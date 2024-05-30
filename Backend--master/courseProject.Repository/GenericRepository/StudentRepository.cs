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

        public async Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(Guid Studentid)
        {
         return await  dbContext.studentCourses.Include(x=>x.Course).Where(x => x.StudentId == Studentid && x.status.ToLower()=="joind").ToListAsync();
        }

        public async Task<IReadOnlyList<Student>> GetAllStudentsInTheSameCourseAsync(Guid courseId)
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

        public async Task addFeedbackAsync(Feedback feedback)
        {
            await dbContext.feedbacks.AddAsync(feedback);
        }

        public async Task<IReadOnlyList<Feedback>> GetAllFeedbacksAsync()
        {
            return await dbContext.feedbacks.Include(x => x.User).Include(x => x.User.student).ToListAsync();
        }

        public async Task<IReadOnlyList<Feedback>> GetFeedbacksByTypeAsync(string type)
        {
            return await dbContext.feedbacks.Include(x=>x.User).ThenInclude(x=>x.student).Where(x=>x.type==type).ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid id)
        {
           return await dbContext.feedbacks.Include(x=>x.User).ThenInclude(x=>x.student).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student> getStudentByIdAsync(Guid? id )
        {          
            return await dbContext.students.FirstOrDefaultAsync(x => x.StudentId == id);
        }

        public async Task<IReadOnlyList<StudentConsultations>> GetAllLectureByStudentIdAsync(Guid StudentId)
        {
            return await dbContext.StudentConsultations.Include(x => x.consultation.instructor)                                                     
                                                       .Include(x=>x.Student.user)
                                                       .Where(x => x.StudentId == StudentId).ToListAsync();
        }

        public async Task<IReadOnlyList<Consultation>> GetAllPublicConsultationsAsync()
        {
            return await dbContext.consultations.Where(x=>x.type.ToLower()=="public").ToListAsync();
        }

        public async Task<IReadOnlyList<StudentConsultations>> GetAllConsultations()
        {
            return await dbContext.StudentConsultations
                .Where(x => x.consultation.type.ToLower() == "public")
                                                       .Include(x => x.consultation)
                                                               .ThenInclude(x=>x.instructor)
                                                               .ThenInclude(x=>x.user)                                                      
                                                       .Include(x=>x.Student)
                                                               .ThenInclude(x=>x.user)
                                                      // .DistinctBy(x=>x.consultationId)
                                                       .ToListAsync();
        }

        public async Task<IReadOnlyList<StudentConsultations>> GetAllOtherPrivateConsultationsAsync(Guid studentId)
        {
            return await dbContext.StudentConsultations.Where(x=>x.consultation.type.ToLower()=="private")
                                                       .Where(x=>x.StudentId!=studentId)
                                                       .Include(x => x.consultation)
                                                               .ThenInclude(x => x.instructor)
                                                               .ThenInclude(x => x.user)
                                                       .Include(x => x.Student)
                                                               .ThenInclude(x => x.user)
                                                       .ToListAsync();
        }

        public async Task<IReadOnlyList<StudentConsultations>> GetAllBookedPrivateConsultationsAsync(Guid studentId)
        {
            return await dbContext.StudentConsultations.Where(x => x.consultation.type.ToLower() == "private")
                                                       .Where(x => x.StudentId == studentId)
                                                       .Include(x => x.consultation)
                                                               .ThenInclude(x => x.instructor)
                                                               .ThenInclude(x => x.user)
                                                       .Include(x => x.Student)
                                                               .ThenInclude(x => x.user)
                                                       .ToListAsync();
        }

        public async Task<List<StudentConsultations>> GetAllStudentsInPublicConsulations(Guid consultationId)
        {
            return await dbContext.StudentConsultations.Where(x=>x.consultationId== consultationId)
                .Where(x => x.consultation.type.ToLower() == "public")
                .Include(x=>x.Student).ThenInclude(x=>x.user).ToListAsync();
        }

        public async Task<Consultation> GetConsultationById(Guid? consultationId)
        {
           return await dbContext.consultations.Include(x=>x.student).ThenInclude(x=>x.user)
                .Include(x=>x.instructor).ThenInclude(x=>x.user).FirstOrDefaultAsync(x=>x.Id==consultationId);
        }

        public async Task<IReadOnlyList<Course>> GetAllCoursesAsync(Guid studentId)
        {
            var allCourses= await dbContext.courses.Include(x => x.studentCourses) .Include(x=>x.Instructor)
                                                                                        .ThenInclude(x=>x.user)
                                                                                   .Include(x=>x.SubAdmin)
                                                                                         .ThenInclude(x=>x.user). ToListAsync();
            if (allCourses.Count > 0)
            {

                StudentCourse studentCourse ;
                foreach (var course in allCourses)
                {
                    studentCourse = course.studentCourses.FirstOrDefault(x=>x.StudentId==studentId && course.Id ==x.courseId );

                    if (studentCourse != null && course.studentCourses.Any(x => x.courseId == course.Id && x.StudentId == studentId && (x.Course.Deadline >= DateTime.Today.Date || x.Course.Deadline == null)))
                    {

                        studentCourse.isEnrolled= true;
                    }
                    //else
                    //{
                    //    studentCourse.isEnrolled = false;
                    //}
                }
            }
            return allCourses;
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
