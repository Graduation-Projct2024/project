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

        public async Task AddMaterial(CourseMaterial courseMaterial)
        {
            await dbContext.Set<CourseMaterial>().AddAsync(courseMaterial);
        }

        public async Task DeleteMaterial(int id)
        {
            var materail= await dbContext.courseMaterials.FirstOrDefaultAsync(x => x.Id == id);
             dbContext.courseMaterials.Remove(materail);
        }

        public async Task EditMaterial( CourseMaterial courseMaterial)
        {
             dbContext.Set<CourseMaterial>().Update(courseMaterial);
        }

        public async Task<IReadOnlyList<Course>> GetAllCoursesGivenByInstructorIdAsync(int Instructorid)
        {
           return  await dbContext.courses.Where(x=>x.InstructorId== Instructorid && (x.status.ToLower()== "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finished")).ToListAsync();
        }


        public async Task AddOfficeHoursAsync(Instructor_Working_Hours instructor_Working_Hours)
        {
            await dbContext.Set<Instructor_Working_Hours>().AddAsync(instructor_Working_Hours);
        }

        public async Task<IReadOnlyList<Instructor_Working_Hours>> GetOfficeHourByIdAsync(int instructorId)
        {
            return  await dbContext.Instructor_Working_Hours.Where(x=>x.InstructorId == instructorId).ToListAsync();
        }

        public async Task<IReadOnlyList<Student_Task_Submissions>> GetAllSubmissionsByTaskIdAsync(int taskId)
        {
            return await dbContext.Student_Task_Submissions
                .Include(x=>x.Student)
                .ThenInclude(x=>x.user)
                .Where(x=>x.TaskId== taskId).ToListAsync();
        }

        public async Task<IReadOnlyList<Consultation>> GetAllConsultationRequestByInstructorIdAsync(int instructorId)
        {
           return await dbContext.consultations.Include(x=>x.student.user).Include(x => x.instructor.user).Where(x=>x.InstructorId == instructorId).ToListAsync();
        }

        public async Task<Instructor> getInstructorByIdAsync(int id)
        {
            return await dbContext.instructors.FirstOrDefaultAsync(x => x.InstructorId == id);
        }

        public async Task<IReadOnlyList<Instructor_Working_Hours>> getAllInstructorsOfficeHoursAsync()
        {
           return await dbContext.Instructor_Working_Hours.Include(x=>x.instructor).ThenInclude(x=>x.user).ToListAsync();
        }

        public async Task AddListOfSkillsAsync(int instructorId, List<int> skills)
        {
            InstructorSkills instructorSkills= new InstructorSkills();
            foreach(var skill in skills)
            {
                instructorSkills.InstructorId=instructorId;
                instructorSkills.skillId = skill;
                await dbContext.InstructorSkills.AddAsync(instructorSkills);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<string>> GetAllSkillsNameToInstructorAsync(List<int> skills)
        {
            List<string> allSkills=new List<string>();
            foreach(var skill in skills)
            {
              allSkills.Add((await dbContext.Skills.FirstOrDefaultAsync(x => x.Id == skill)).name);
            }
            return (IReadOnlyList<string>)allSkills;
        }

        public async Task<IReadOnlyList<Instructor_Working_Hours>> showifSelectedTimeIsAvilable(TimeSpan startTime , TimeSpan endTime , DateTime date)
        {
           return await  dbContext.Instructor_Working_Hours.Include(x=>x.instructor).ThenInclude(x=>x.Consultations)
                .Where(x=>x.day == date.DayOfWeek)
                .Where(x=> x.startTime <= startTime && x.endTime >= endTime)
                .Where(x => !x.instructor.Consultations.Any(y => 
        y.date == date.Date && (
            (startTime >= y.startTime && startTime < y.endTime) || 
            (endTime > y.startTime && endTime <= y.endTime) || 
            (startTime <= y.startTime && endTime >= y.endTime) 
        )))
    .ToListAsync();
                //.Where(x=>!x.instructor.Consultations.Any(y=> y.date==date.Date && y.startTime==startTime && y.endTime==endTime))
                //.Where(x=>!x.instructor.Consultations.Any(y=> startTime>y.startTime && startTime<y.endTime && y.date==date))
                //.Where(x => !x.instructor.Consultations.Any(y => endTime > y.startTime && endTime < y.endTime && y.date==date))
                //.Where(x=>x.day == date.DayOfWeek &&x.startTime>= startTime&&x.endTime <=endTime  ).ToListAsync();                 
        }

        public async Task RemoveASkill(InstructorSkills instructorSkills)
        {
             dbContext.InstructorSkills.Remove(instructorSkills);
        }

        public async Task<IReadOnlyList<InstructorSkills>> GetAllInstructorSkillsRecoredsAsync()
        {
          return  await dbContext.InstructorSkills.ToListAsync();
        }

        public async Task<IReadOnlyList<Skills>> getAllUnregisteredSkillsOfTheInstructor(int instructorId)
        {
            return await dbContext.Skills.Where(x=>!x.instructorSkills.Any(y=>y.InstructorId == instructorId)).ToListAsync();
        }

        public async Task<IReadOnlyList<Instructor_Working_Hours>> getAListOfInstructorDependOnSkillsAndOfficeTime( int skillID, TimeSpan startTime, TimeSpan endTime, DateTime date )
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

        public async Task<IReadOnlyList<Skills>> getAllInstructorSkills(int instructorId)
        {
            return await dbContext.Skills.Where(x=>x.instructorSkills.Any(y=>y.InstructorId == instructorId)).ToListAsync();
        }

        public async Task removeInstructor(Instructor instructor)
        {
             dbContext.instructors.Remove(instructor);
        }
    }
}
