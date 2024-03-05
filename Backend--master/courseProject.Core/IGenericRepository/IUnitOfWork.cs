using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IUnitOfWork
    {

        public ISubAdminRepository SubAdminRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IinstructorRepositpry instructorRepositpry { get; set; }
    }
}
