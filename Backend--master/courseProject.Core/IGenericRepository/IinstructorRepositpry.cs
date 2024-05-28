using courseProject.core.Models;
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
        public Task DeleteMaterial(Guid id);
        public Task EditMaterial ( CourseMaterial courseMaterial);
        public Task<IReadOnlyList<Course>> GetAllCoursesGivenByInstructorIdAsync(Guid Instructorid);
        public Task AddOfficeHoursAsync(Instructor_Working_Hours instructor_Working_Hours);
        public Task<IReadOnlyList<Instructor_Working_Hours>> GetOfficeHourByIdAsync(Guid instructorId);
        public Task<IReadOnlyList<Student_Task_Submissions>> GetAllSubmissionsByTaskIdAsync(Guid taskId);
        public Task<IReadOnlyList<Consultation>> GetAllConsultationRequestByInstructorIdAsync(Guid instructorId);
        public Task<Instructor> getInstructorByIdAsync(Guid id);
        public Task<IReadOnlyList<Instructor_Working_Hours>> getAllInstructorsOfficeHoursAsync();
        public Task AddListOfSkillsAsync(Guid instructorId, List<Guid> skills);
        public Task<IReadOnlyList<string>> GetAllSkillsNameToInstructorAsync(List<Guid> skills);
        public Task<IReadOnlyList<Instructor_Working_Hours>> showifSelectedTimeIsAvilable(TimeSpan startTime, TimeSpan endTime , DateTime date);
        public Task RemoveASkill(InstructorSkills instructorSkills);
        public Task<IReadOnlyList<InstructorSkills>> GetAllInstructorSkillsRecoredsAsync();
        public Task<IReadOnlyList<Skills>> getAllUnregisteredSkillsOfTheInstructor(Guid instructorId);
        public Task<IReadOnlyList<Instructor_Working_Hours>> getAListOfInstructorDependOnSkillsAndOfficeTime(Guid skillID, TimeSpan startTime, TimeSpan endTime, DateTime date );
        public Task<IReadOnlyList<Skills>> getAllInstructorSkills(Guid instructorId);
        public  Task removeInstructor(Instructor instructor);
       

    }
}
