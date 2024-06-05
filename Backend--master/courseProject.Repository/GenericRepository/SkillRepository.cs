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
    public class SkillRepository : ISkillRepository
    {
        private readonly projectDbContext dbContext;

        public SkillRepository(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task addSkillOptionsAsync(Skills skill)
        {
            await dbContext.Set<Skills>().AddAsync(skill);
        }



        public async Task<IReadOnlyList<Skills>> GetAllSkillsAsync()
        {
            return await dbContext.Skills.ToListAsync();
        }


        public async Task AddListOfSkillsAsync(Guid instructorId, List<Guid> skills)
        {
            InstructorSkills instructorSkills = new InstructorSkills();
            foreach (var skill in skills)
            {
                instructorSkills.InstructorId = instructorId;
                instructorSkills.skillId = skill;
                await dbContext.InstructorSkills.AddAsync(instructorSkills);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<string>> GetAllSkillsNameToInstructorAsync(List<Guid> skills)
        {
            List<string> allSkills = new List<string>();
            foreach (var skill in skills)
            {
                allSkills.Add((await dbContext.Skills.FirstOrDefaultAsync(x => x.Id == skill)).name);
            }
            return (IReadOnlyList<string>)allSkills;
        }

        public async Task RemoveASkill(InstructorSkills instructorSkills)
        {
            dbContext.InstructorSkills.Remove(instructorSkills);
        }


        public async Task<IReadOnlyList<Skills>> getAllUnregisteredSkillsOfTheInstructor(Guid instructorId)
        {
            return await dbContext.Skills.Where(x => !x.instructorSkills.Any(y => y.InstructorId == instructorId)).ToListAsync();
        }

        public async Task<IReadOnlyList<Skills>> getAllInstructorSkills(Guid instructorId)
        {
            return await dbContext.Skills.Where(x => x.instructorSkills.Any(y => y.InstructorId == instructorId)).ToListAsync();
        }

    }
}
