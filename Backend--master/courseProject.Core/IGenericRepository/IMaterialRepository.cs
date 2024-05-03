﻿using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IMaterialRepository :IGenericRepository1<CourseMaterial>
    {
        public Task<CourseMaterial> GetMaterialByIdAsync(int materialId);
        public Task<IEnumerable<CourseMaterial>> GetAllMaterialInSameCourse(int CourseId);
    }
}
