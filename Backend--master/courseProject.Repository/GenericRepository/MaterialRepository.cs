using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class MaterialRepository : GenericRepository1<CourseMaterial>, IMaterialRepository
    {
        public MaterialRepository(projectDbContext dbContext) : base(dbContext)
        {
        }
    }
}
