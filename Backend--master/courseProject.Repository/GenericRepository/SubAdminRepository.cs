using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class SubAdminRepository : GenericRepository1<SubAdmin>, ISubAdminRepository
    {
        private readonly projectDbContext dbContext;
        private IDbContextTransaction _currentTransaction;

        public SubAdminRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
         
        }

      

      

        
       

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return null;
            }

            _currentTransaction = await dbContext.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }


        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

      

        public async Task<SubAdmin> GetSubAdminByIdAsync(Guid id)
        {
           return await dbContext.subadmins.Include(x=>x.user).FirstOrDefaultAsync(x => x.SubAdminId == id);
 
        }

       

        public async Task<SubAdmin> getSubAdminByIdAsync(Guid subAdminId)
        {
            return await dbContext.subadmins.FirstOrDefaultAsync(x => x.SubAdminId == subAdminId);
        }

       

        public async Task RemoveSubAdmin(SubAdmin subAdmin)
        {
            dbContext.subadmins.Remove(subAdmin);
        }

        public async Task editRole(User user)
        {
            dbContext.users.Update(user);
        }

        //public async Task<Event> GetEventById(int id)
        //{
        //   return await dbContext.events.FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}
