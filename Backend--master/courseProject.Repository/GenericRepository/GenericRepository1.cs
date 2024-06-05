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
using courseProject.Core.Models.DTO;

namespace courseProject.Repository.GenericRepository
{
    public class GenericRepository1<T> : IGenericRepository1<T> where T : class
    {
        private readonly projectDbContext dbContext;
       

        public GenericRepository1(projectDbContext dbContext)
        {
           
            this.dbContext = dbContext;
        }


        public async void DetachEntity(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Detached;
        }

        public async void AttachEntity(Course entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IReadOnlyList<T>> GetAllStudentsAsync()
        {
            if (typeof(T) == typeof(Student))
            {
                return (IReadOnlyList<T>)await dbContext.students.Include(x => x.user).Where(x => x.user.IsVerified == true).ToListAsync();

            }
            return await dbContext.Set<T>().ToListAsync();

        }


        public async Task<IReadOnlyList<T>> GetAllEmployeeAsync()
        {
            if (typeof(T) == typeof(SubAdmin))
            {
                return (IReadOnlyList<T>)await dbContext.subadmins.Include(x => x.user).Where(x => x.user.IsVerified == true).ToListAsync();
            }
            else if (typeof(T) == typeof(Instructor))
            {
                return (IReadOnlyList<T>)await dbContext.instructors.Include(x => x.user).Where(x => x.user.IsVerified == true).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }

        //edit status from On to Accredit 
        public async Task<IReadOnlyList<T>> GetAllCoursesAsync()
        {
            if (typeof(T) == typeof(Course))
            {
                
                return (IReadOnlyList<T>) await dbContext.courses
                    .Where(x=>x.status== "accredit")
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




        public async Task<IReadOnlyList<T>> GetAllCoursesForAccreditAsync()
        {
            if(typeof(T) == typeof(Course))
            {
                return (IReadOnlyList<T>) await dbContext.courses
                   
                    .Include(x => x.SubAdmin.user).Include(x => x.Instructor.user).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllEventsForAccreditAsync()
        {
            if(typeof(T) == typeof(Event))
            {
                return (IReadOnlyList<T>)await dbContext.events
                    
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
            dbContext.Entry(entity).State = EntityState.Modified;
          //  dbContext.Set<T>().Update(entity);
            
        }

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


    

      


        public async Task<T> ViewProfileAsync(Guid id ,string role)
        {
            if(role.ToLower() == "admin")
            {
              return (T)(object) await dbContext.users.Include(x => x.admin).FirstOrDefaultAsync(x => x.UserId == id);
            }
            if (role.ToLower() == "subadmin" || role.ToLower() == "main-subadmin")
            {
                return (T)(object)await dbContext.users.Include(x => x.subadmin).FirstOrDefaultAsync(x => x.UserId == id);
            }
            if (role.ToLower() == "instructor")
            {
                return (T)(object)await dbContext.users.Include(x => x.instructor).FirstOrDefaultAsync(x => x.UserId == id);
            }
            if (role.ToLower() == "student")
            {
                return (T)(object)await dbContext.users.Include(x => x.student).FirstOrDefaultAsync(x => x.UserId == id);
            }
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<User> GetAdminId()
        {
            return await dbContext.users.FirstOrDefaultAsync(x=>x.role.ToLower()=="admin");
        }




        //public async Task<T> GetUserInformationByIdAsync(int id)
        //{
        //    if(typeof(T) == typeof(Admin))
        //    {
        //        return 
        //    }
        //}


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
