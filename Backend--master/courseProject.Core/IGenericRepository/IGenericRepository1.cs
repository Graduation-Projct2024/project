using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IGenericRepository1<T> where T : class
    {

        Task<IEnumerable<T>> GetAllStudentsAsync();
        Task<IEnumerable<T>> GetAllEmployeeAsync();
        Task<IReadOnlyList<T>> GetAllCoursesAsync();
        Task<IReadOnlyList<T>> GetAllEventsAsync();

        Task<IReadOnlyList<T>> GetAllStudentsForContactAsync();
        Task<IReadOnlyList<T>> GetAllEmployeeForContactAsync();
        Task<T> GetEmployeeById(int id);

        Task<IReadOnlyList<T>> GetAllCoursesForAccreditAsync();
        Task<IReadOnlyList<T>> GetAllEventsForAccreditAsync();

        Task createSubAdminAccountAsync(T entity);
       public Task<int> saveAsync();

        Task createInstructorAccountAsync(T entity);
        Task updateSubAdminAsync(T entity);
        Task<IEnumerable<T>> GetAllMaterialInSameCourse(int CourseId);
        public Task<T> GetMaterialByIdAsync(int id);
        public Task<T> EditProfile(int id, ProfileDTO profile);

    }
}
