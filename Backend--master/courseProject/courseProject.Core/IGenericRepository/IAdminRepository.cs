using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IAdminRepository :IGenericRepository1<Admin>
    {
        public Task CreateAdminAccountAsync(Admin admin);

        public Task<Admin> GetAdminByIdAsync(Guid id);
        public Task addSkillOptionsAsync(Skills skill);
        public Task<IReadOnlyList< Skills>> GetAllSkillsAsync();
    }
}
