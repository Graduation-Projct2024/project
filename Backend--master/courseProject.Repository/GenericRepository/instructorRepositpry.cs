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
           return  await dbContext.courses.Where(x=>x.InstructorId== Instructorid && x.status.ToLower()== "accredit").ToListAsync();
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
                .Include(x=>x.Student.user)
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
           return await dbContext.Instructor_Working_Hours.Include(x=>x.instructor).Include(x=>x.instructor.user).ToListAsync();
        }
    }
}
