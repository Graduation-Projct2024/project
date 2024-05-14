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
    public class AdminRepository : GenericRepository1<Admin>, IAdminRepository
    {
        private readonly projectDbContext dbContext;

        public AdminRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task addSkillOptionsAsync(Skills skill)
        {
            await dbContext.Set<Skills>().AddAsync(skill);  
        }

        public async Task CreateAdminAccountAsync(Admin admin)
        {
            await dbContext.Set<Admin>().AddAsync(admin);
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            return await dbContext.admins.FirstOrDefaultAsync(x => x.AdminId == id);
        }

        public async Task<IReadOnlyList< Skills>> GetAllSkillsAsync()
        {
            return await dbContext.Skills.ToListAsync();
        }
    }
}
