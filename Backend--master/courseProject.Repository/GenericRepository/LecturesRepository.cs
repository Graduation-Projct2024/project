using courseProject.Core.IGenericRepository;
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
    public class LecturesRepository : ILecturesRepository
    {
        private readonly projectDbContext dbContext;

        public LecturesRepository(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IReadOnlyList<Instructor_Working_Hours>> showifSelectedTimeIsAvilable(TimeSpan startTime, TimeSpan endTime, DateTime date)
        {
            return await dbContext.Instructor_Working_Hours.Include(x => x.instructor).ThenInclude(x => x.Consultations)
                 .Where(x => x.day == date.DayOfWeek)
                 .Where(x => x.startTime <= startTime && x.endTime >= endTime)
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


        public async Task BookLectureAsync(Consultation consultation)
        {
            await dbContext.Set<Consultation>().AddAsync(consultation);
        }



        public async Task<IReadOnlyList<StudentConsultations>> GetAllLectureByStudentIdAsync(Guid StudentId)
        {
            return await dbContext.StudentConsultations.Include(x => x.consultation.instructor)
                                                       .Include(x => x.Student.user)
                                                       .Where(x => x.StudentId == StudentId).ToListAsync();
        }

        public async Task<IReadOnlyList<Consultation>> GetAllPublicConsultationsAsync()
        {
            return await dbContext.consultations.Where(x => x.type.ToLower() == "public").ToListAsync();
        }

        public async Task<IReadOnlyList<StudentConsultations>> GetAllPublicConsultations()
        {
            return await dbContext.StudentConsultations
                .Where(x => x.consultation.type.ToLower() == "public")
                                                       .Include(x => x.consultation)
                                                               .ThenInclude(x => x.instructor)
                                                               .ThenInclude(x => x.user)
                                                       .Include(x => x.Student)
                                                               .ThenInclude(x => x.user)
                                                       // .DistinctBy(x=>x.consultationId)
                                                       .ToListAsync();
        }

        public async Task<IReadOnlyList<StudentConsultations>> GetAllOtherPrivateConsultationsAsync(Guid studentId)
        {
            return await dbContext.StudentConsultations.Where(x => x.consultation.type.ToLower() == "private")
                                                       .Where(x => x.StudentId != studentId)
                                                       .Include(x => x.consultation)
                                                               .ThenInclude(x => x.instructor)
                                                               .ThenInclude(x => x.user)
                                                       .Include(x => x.Student)
                                                               .ThenInclude(x => x.user)
                                                       .ToListAsync();
        }

        public async Task<IReadOnlyList<StudentConsultations>> GetAllBookedPrivateConsultationsAsync(Guid studentId)
        {
            return await dbContext.StudentConsultations.Where(x => x.consultation.type.ToLower() == "private")
                                                       .Where(x => x.StudentId == studentId)
                                                       .Include(x => x.consultation)
                                                               .ThenInclude(x => x.instructor)
                                                               .ThenInclude(x => x.user)
                                                       .Include(x => x.Student)
                                                               .ThenInclude(x => x.user)
                                                       .ToListAsync();
        }



        public async Task<Consultation> GetConsultationById(Guid? consultationId)
        {
            return await dbContext.consultations.Include(x => x.student).ThenInclude(x => x.user)
                 .Include(x => x.instructor).ThenInclude(x => x.user).FirstOrDefaultAsync(x => x.Id == consultationId);
        }


    }
}
