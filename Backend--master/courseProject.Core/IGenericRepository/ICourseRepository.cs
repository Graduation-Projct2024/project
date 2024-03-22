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
        public  Task<T> GetCourseByIdAsync(int id);
    }
}
