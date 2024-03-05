using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public  interface IinstructorRepositpry :IGenericRepository1<Instructor>
    {
        public Task AddMaterial(CourseMaterial courseMaterial);
        public Task DeleteMaterial(int id);
    }
}
