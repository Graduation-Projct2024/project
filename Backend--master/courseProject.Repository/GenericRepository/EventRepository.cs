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

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await dbContext.events.FirstOrDefaultAsync(x => x.Id == eventId);
        }
    }
}
