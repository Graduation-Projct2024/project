using Microsoft.EntityFrameworkCore.Storage;
using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface ISubAdminRepository:IGenericRepository1<SubAdmin>
    {

     
     
      

     
        public  Task<IDbContextTransaction> BeginTransactionAsync();
        public  Task CommitTransactionAsync();

        public Task RollbackTransactionAsync();
        public Task<SubAdmin> GetSubAdminByIdAsync(Guid id); 
    
       
        public Task<SubAdmin> getSubAdminByIdAsync(Guid subAdminId);
     
        public Task RemoveSubAdmin(SubAdmin subAdmin);
        public Task editRole(User user);
       // public Task<Event> GetEventById(int id);

    }
}
