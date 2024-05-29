﻿using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class MaterialRepository : GenericRepository1<CourseMaterial>, IMaterialRepository
    {
        private readonly projectDbContext dbContext;

        public MaterialRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CourseMaterial> GetMaterialByIdAsync(Guid id)
        {
           
                return await dbContext.courseMaterials.FirstOrDefaultAsync(x => x.Id == id);
            
            
        }

        public async Task<IReadOnlyList<CourseMaterial>> GetAllMaterial(Guid? Courseid, Guid? consultationId)
        {
            return await dbContext.courseMaterials.Where(x => x.courseId == Courseid || x.consultationId == consultationId).ToListAsync();
        }

        public async Task<IEnumerable<CourseMaterial>> GetAllMaterialInSameCourse(Guid courseId)
        {
            
                return (IEnumerable<CourseMaterial>)
                await dbContext.courseMaterials                                                 
                .Where(a => a.courseId == courseId).ToListAsync();
           
        }
    }
}
