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

        public async Task CreateCourse(Course model)
        {
            await dbContext.Set<Course>().AddAsync(model);
            
        }

        public async Task CreateRequest(Request request)
        {
            await dbContext.Set<Request>().AddAsync(request);
        }

        public async Task updateCourse(Course course)
        {
             dbContext.Update(course);
        }
        public async Task updateEvent(Event model)
        {
            dbContext.Update(model);
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

        public async Task CreateEvent(Event model)
        {
            await dbContext.Set<Event>().AddAsync(model);
        }

        public async Task<SubAdmin> GetSubAdminByIdAsync(int id)
        {
           return await dbContext.subadmins.FirstOrDefaultAsync(x => x.SubAdminId == id);
 
        }

        public async Task<IReadOnlyList<Request>> GerAllCoursesRequestAsync()
        {
            return await dbContext.requests.Include(x => x.Student.user).Where(x => x.satus == "custom-course").ToListAsync(); 
        }
        public async Task<Request> GerCourseRequestByIdAsync(int id)
        {
             return await dbContext.requests.Include(x => x.Student.user).Where(x => x.satus == "custom-course").FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<SubAdmin> getSubAdminByIdAsync(int subAdminId)
        {
            return await dbContext.subadmins.FirstOrDefaultAsync(x => x.SubAdminId == subAdminId);
        }

        //public async Task<Event> GetEventById(int id)
        //{
        //   return await dbContext.events.FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}
