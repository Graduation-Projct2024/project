using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO;


namespace courseProject.Core.IGenericRepository
{
    public interface IUserRepository
    {

     Task< LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO); 
     Task<User>  RegisterAsync(RegistrationRequestDTO registerRequestDTO);

       bool isUniqeUser(string email);

        //Task LogOut();
    }
}
