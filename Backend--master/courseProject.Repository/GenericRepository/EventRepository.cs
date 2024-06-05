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
    public class EventRepository : GenericRepository1<Event>, IEventRepository
    {
        private readonly projectDbContext dbContext;

        public EventRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await dbContext.events.FirstOrDefaultAsync(x => x.Id == eventId);
        }

        public async Task<IReadOnlyList<Event>> GetAllUndefindEventBySubAdminIdAsync(Guid subAdminId)
        {
            return await dbContext.events.Include(e => e.SubAdmin).ThenInclude(s => s.user)
                 .Where(e => e.SubAdminId == subAdminId && e.status.ToLower() == "undefined").ToListAsync();
        }

        public async Task CreateEvent(Event model)
        {
            await dbContext.Set<Event>().AddAsync(model);
        }


        public async Task updateEvent(Event model)
        {
            dbContext.Update(model);
        }




    }
}
