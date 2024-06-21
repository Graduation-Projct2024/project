using Microsoft.EntityFrameworkCore;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;


namespace courseProject.Repository.GenericRepository
{
    public class GenericRepository1<T> : IGenericRepository1<T> where T : class
    {
        private readonly projectDbContext dbContext;
        private IDbContextTransaction _currentTransaction;


        public GenericRepository1(projectDbContext dbContext )
        {
           
            this.dbContext = dbContext;
         
        }


      

       


        public async Task<IReadOnlyList<T>> GetAllEmployeeAsync()
        {
            if (typeof(T) == typeof(SubAdmin))
            {
                return (IReadOnlyList<T>)await dbContext.subadmins.Include(x => x.user).Where(x=>x.SubAdminId==x.user.UserId).Where(x => x.user.IsVerified == true).ToListAsync();
            }
            else if (typeof(T) == typeof(Instructor))
            {
                return (IReadOnlyList<T>)await dbContext.instructors.Include(x => x.user).Where(x => x.InstructorId == x.user.UserId).Where(x => x.user.IsVerified == true).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }


       

        public async Task<int> saveAsync()
        => await dbContext.SaveChangesAsync();

        


        public async Task<T> GetEmployeeById(Guid id)
        {
            if (typeof(T) == typeof(SubAdmin))
            {
                return (T)(object) await dbContext.subadmins.Include(x=>x.user).FirstOrDefaultAsync(a => a.SubAdminId==id);
               
            }
            else if (typeof(T) == typeof(Instructor))
            {
                return (T)(object) await dbContext.instructors.Include(x => x.user).FirstOrDefaultAsync(a => a.InstructorId == id);

            }
            return await dbContext.Set<T>().FindAsync(id);
        }


        public async Task updateEmployeeAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            
        }

        //Begins a new transaction asynchronously if there is no current transaction.
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return null;
            }

            _currentTransaction = await dbContext.Database.BeginTransactionAsync();
            return _currentTransaction;
        }



       

    }
}
