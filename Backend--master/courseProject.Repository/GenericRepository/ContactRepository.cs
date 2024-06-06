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
    public class ContactRepository : GenericRepository1<Contact> , IContactRepository
    {
        private readonly projectDbContext dbContext;

        public ContactRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddMessageContactasync(Contact contact)
        {
            await dbContext.Set<Contact>().AddAsync(contact);
        }

        public async Task<IReadOnlyList<Contact>> GetAllContactsAsync()
        {
           return await dbContext.contacts.ToListAsync();
        }

        public async Task<Contact> getContactByIdAsync(Guid id)
        {
           return await dbContext.contacts.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
