using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IMaterialRepository :IGenericRepository1<CourseMaterial>
    {
        public Task<CourseMaterial> GetMaterialByIdAsync(Guid materialId);
        public Task<IEnumerable<CourseMaterial>> GetAllMaterialInSameCourse(Guid CourseId);

        public Task<IReadOnlyList<CourseMaterial>> GetAllMaterial(Guid? Courseid , Guid? consultationId);
    }
}
