using Microsoft.EntityFrameworkCore;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.core.Models;
using courseProject.Core.Models.DTO;

namespace courseProject.Repository.GenericRepository
{
    public class instructorRepositpry : GenericRepository1<Instructor>, IinstructorRepositpry
    {
        private readonly projectDbContext dbContext;

        public instructorRepositpry(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

       
       


        public async Task AddOfficeHoursAsync(Instructor_Working_Hours instructor_Working_Hours)
        {
            await dbContext.Set<Instructor_Working_Hours>().AddAsync(instructor_Working_Hours);
        }

        public async Task<IReadOnlyList<Instructor_Working_Hours>> GetOfficeHourByIdAsync(Guid instructorId)
        {
            return  await dbContext.Instructor_Working_Hours.Where(x=>x.InstructorId == instructorId).ToListAsync();
        }

      

        

        public async Task<Instructor> getInstructorByIdAsync(Guid id)
        {
            return await dbContext.instructors.Include(x=>x.user).FirstOrDefaultAsync(x => x.InstructorId == id);
        }

        public async Task<IReadOnlyList<Instructor_Working_Hours>> getAllInstructorsOfficeHoursAsync()
        {
           return await dbContext.Instructor_Working_Hours.Include(x=>x.instructor).ThenInclude(x=>x.user).ToListAsync();
        }

       

       
        

       

        public async Task<IReadOnlyList<InstructorSkills>> GetAllInstructorSkillsRecoredsAsync()
        {
          return  await dbContext.InstructorSkills.ToListAsync();
        }

       

        public async Task<IReadOnlyList<Instructor_Working_Hours>> getAListOfInstructorDependOnSkillsAndOfficeTime(Guid skillID, TimeSpan startTime, TimeSpan endTime, DateTime date )
        {
            return await dbContext.Instructor_Working_Hours.Include(x => x.instructor).ThenInclude(x => x.Consultations)
                .Include(x=>x.instructor.user)
                
                .Where(x => x.day == date.DayOfWeek)
                .Where(x => x.startTime <= startTime && x.endTime >= endTime)
                .Where(x=>x.instructor.instructorSkills.Any(y=>y.skillId ==skillID))
                .Where(x => !x.instructor.Consultations.Any(y =>
        y.date == date.Date && (
            (startTime >= y.startTime && startTime < y.endTime) ||
            (endTime > y.startTime && endTime <= y.endTime) ||
            (startTime <= y.startTime && endTime >= y.endTime)
        )))
    .ToListAsync();
        }

        

      
    }
}
