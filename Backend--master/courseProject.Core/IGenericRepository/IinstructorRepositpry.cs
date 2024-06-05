﻿using courseProject.core.Models;
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
       
       
        public Task AddOfficeHoursAsync(Instructor_Working_Hours instructor_Working_Hours);
        public Task<IReadOnlyList<Instructor_Working_Hours>> GetOfficeHourByIdAsync(Guid instructorId);
      
       
        public Task<Instructor> getInstructorByIdAsync(Guid id);
        public Task<IReadOnlyList<Instructor_Working_Hours>> getAllInstructorsOfficeHoursAsync();
      
      
      
        
        public Task<IReadOnlyList<InstructorSkills>> GetAllInstructorSkillsRecoredsAsync();

        public Task<IReadOnlyList<Instructor_Working_Hours>> getAListOfInstructorDependOnSkillsAndOfficeTime(Guid skillID, TimeSpan startTime, TimeSpan endTime, DateTime date );
       
      
       

    }
}
