using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface ICourseRepository<T> :IGenericRepository1<Course>
    {
        public  Task<T> GetCourseByIdAsync(Guid id);
        public Task<int> GetNumberOfStudentsInTHeCourseAsync(Guid courseId);
        public Task EditCourseAfterAccreditAsync(Guid courseId);
        public Task UpdateStudentCourse(StudentCourse studentCourse);
        public Task<IReadOnlyList<Course>> GetAllUndefinedCoursesBySubAdminIdAsync(Guid subAdminId);
        public Task<Course> getAccreditCourseByIdAcync(Guid courseId);
    }
}
