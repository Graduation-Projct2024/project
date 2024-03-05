using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class GenericRepository1<T> : IGenericRepository1<T> where T : class
    {
        private readonly projectDbContext dbContext;
        //SqlConnection connection;
        //SqlCommand command;

        public GenericRepository1(projectDbContext dbContext)
        {
           
            this.dbContext = dbContext;
        }

        //public void DBConnectionOpen()
        //{
        //    connection = new SqlConnection();
        //   // connection.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        //    if (connection.State != ConnectionState.Open)
        //        connection.Open();
        //}
        //public void DBConnectionClose()
        //{
        //    if (connection.State != ConnectionState.Closed)
        //        connection.Close();
        //}

        public async Task<IEnumerable<T>> GetAllStudentsAsync()
        {
            if(typeof(T) == typeof(Student))
            {
                return (IEnumerable<T>)await dbContext.students.Include(x => x.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();

        }


        public async Task<IEnumerable<T>> GetAllEmployeeAsync()
        {
            if(typeof(T) == typeof(SubAdmin))
            {
                return (IEnumerable<T>)await dbContext.subadmins.Include(x => x.user).ToListAsync();
            }
            else if (typeof(T) == typeof(Instructor))
            {
                return (IEnumerable<T>)await dbContext.instructors.Include(x => x.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAllCoursesAsync()
        {
            if (typeof(T) == typeof(Course))
            {
                
                return (IReadOnlyList<T>) await dbContext.courses
                    .Where(x=>x.status=="On")
                    .Include(x=>x.Instructor.user).Include(x=>x.SubAdmin.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllEventsAsync()
        {
            if (typeof(T) == typeof(Event))
            {
                return (IReadOnlyList<T>)await dbContext.events
                    .Where(x=>x.status== "accredit")
                    .Include(x => x.SubAdmin.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAllStudentsForContactAsync()
        {
            if(typeof(T) == typeof(Student))
            {
                return (IReadOnlyList<T>)await dbContext.students.Include(x => x.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAllEmployeeForContactAsync()
        {
            if (typeof(T) == typeof(Instructor))
            {
                return (IReadOnlyList<T>)await dbContext.instructors.Include(x => x.user).ToListAsync();
            }
            else if (typeof(T) == typeof(SubAdmin))
            {
                return (IReadOnlyList<T>) await dbContext.subadmins.Include(x=>x.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAllCoursesForAccreditAsync()
        {
            if(typeof(T) == typeof(Course))
            {
                return (IReadOnlyList<T>) await dbContext.courses
                    .Where(x => x.status == "undefined")
                    .Include(x => x.SubAdmin.user).Include(x => x.Instructor.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllEventsForAccreditAsync()
        {
            if(typeof(T) == typeof(Event))
            {
                return (IReadOnlyList<T>)await dbContext.events
                    .Where(x => x.status == "undefined")
                    .Include(x => x.SubAdmin.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task createSubAdminAccountAsync(T entity)
        {
             await dbContext.Set<T>().AddAsync(entity);
        }

        public async Task<int> saveAsync()
        => await dbContext.SaveChangesAsync();

        public async Task createInstructorAccountAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public async Task updateSubAdminAsync(T entity)
        {
             dbContext.Set<T>().Update(entity);
            
        }

        public async Task<T> GetEmployeeById(int id)
        {
            if (typeof(T) == typeof(SubAdmin))
            {
                return (T)(object) await dbContext.subadmins.Include(x=>x.user).FirstOrDefaultAsync(a => a.Id==id);
               
            }
            else if (typeof(T) == typeof(Instructor))
            {
                return (T)(object) await dbContext.instructors.Include(x => x.user).FirstOrDefaultAsync(a => a.Id == id);

            }
            return await dbContext.Set<T>().FindAsync(id);
        }


        //public async Task<IEnumerable<T>> createSubAdminAccountAsync(SubAdmin subadmin , User user)
        //{
        //   var sub = dbContext.subadmins.Where(x=>x.Id == subadmin.Id).FirstOrDefaultAsync();
        //    var auser = dbContext.users.Where(x => x.email == user.email).FirstOrDefaultAsync();
        //    var subAdminUser = new User()
        //    {
        //        email = user.email,
        //    };
        //    dbContext.users.AddAsync(user);
        //    dbContext.SaveChanges();
        //    dbContext.subadmins.AddAsync(subadmin);
        //    dbContext.SaveChanges();
        //    return true;
        //}

    }
}
