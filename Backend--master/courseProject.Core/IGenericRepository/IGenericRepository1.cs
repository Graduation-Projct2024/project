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

        public  void DetachEntity(T entity);
        Task<IReadOnlyList<T>> GetAllStudentsAsync();
        Task<IReadOnlyList<T>> GetAllEmployeeAsync();
        Task<IReadOnlyList<T>> GetAllCoursesAsync();
        Task<IReadOnlyList<T>> GetAllEventsAsync();

        Task<IReadOnlyList<T>> GetAllStudentsForContactAsync();
       // Task<IReadOnlyList<T>> GetAllEmployeeForContactAsync();
        Task<T> GetEmployeeById(Guid id);

        Task<IReadOnlyList<T>> GetAllCoursesForAccreditAsync();
        Task<IReadOnlyList<T>> GetAllEventsForAccreditAsync();

        Task createSubAdminAccountAsync(T entity);
       public Task<int> saveAsync();


      //  Task<T> GetUserInfoById(int id);
        Task createInstructorAccountAsync(T entity);
        Task updateSubAdminAsync(T entity);
       
       // public Task<T> GetMaterialByIdAsync(int id);
        public Task<T> ViewProfileAsync(Guid id, string role);
        public Task<User> GetAdminId();
      
    }
}
