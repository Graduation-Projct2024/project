using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
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

        public async Task CreateAdminAccountAsync(Admin admin)
        {
            await dbContext.Set<Admin>().AddAsync(admin);
        }
    }
}
