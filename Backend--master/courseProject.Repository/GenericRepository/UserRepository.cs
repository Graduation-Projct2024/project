﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;

namespace courseProject.Repository.GenericRepository
{
    public class UserRepository : GenericRepository1<User>,  IUserRepository
    {
 
        
        private readonly projectDbContext dbContext;
        private string secretKey;
        private string token1 = "";
        public UserRepository( projectDbContext dbContext ,IConfiguration configuration):base(dbContext)
        {
            this.dbContext = dbContext;
            secretKey = configuration.GetSection("Authentication")["SecretKey"]; 
        }

       

        public bool isUniqeUser(string email)
        {
            var user = dbContext.users.FirstOrDefault(x=>x.email == email);
            if(user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await dbContext.users.FirstOrDefaultAsync(x => x.email == loginRequestDTO.email);
            bool pass = false;
            if (user != null)
            {
                pass = BC.Verify(loginRequestDTO.password, user.password);
            }

            if (user == null || pass == false)
            {
                return new LoginResponseDTO() {
                    User = null,
                    Token = ""

                };
            }
           return generateToken(user);
        }
        public LoginResponseDTO generateToken(User? user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",user.UserId.ToString()),
                    new Claim(ClaimTypes.Email ,user.email),

                   new Claim(ClaimTypes.Role , user.role )

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                User = user,
                Token = tokenHandler.WriteToken(token)

            };
            token1 = loginResponseDTO.Token;
            return loginResponseDTO;
        }
        

       
     

        public async Task<User> RegisterAsync( RegistrationRequestDTO registerRequestDTO)
        {
            if(registerRequestDTO.password != registerRequestDTO.ConfirmPassword)
            {
                return null;
            }
            var passHash = BC.HashPassword(registerRequestDTO.password);
            User user = new User()
            {
               
                userName = registerRequestDTO.userName,
                email= registerRequestDTO.email,
                password= passHash,
                role= registerRequestDTO.role.ToLower()
            };
           await dbContext.users.AddAsync(user);
          // await dbContext.SaveChangesAsync();
          //  user.password = "";
            return  user;
        }


        public async Task<User> GetUserByRoleAsync(string role)
        {
           return await dbContext.users.FirstOrDefaultAsync(x=>x.role.ToLower()==role.ToLower());
        }

        public async Task<User> getUserByIdAsync(int UserId)
        {
            return await dbContext.users.FirstOrDefaultAsync(x => x.UserId == UserId);
        }

        public async Task<IReadOnlyList< User>> getAllMainSubAmdinRole()
        {
           return await dbContext.users.Where(x => x.role.ToLower() == "main-subadmin").ToListAsync();
        }
    }
}
