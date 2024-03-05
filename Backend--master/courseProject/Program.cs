using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using courseProject.Core.IGenericRepository;
using courseProject.Repository.Data;
using courseProject.Repository.GenericRepository;
using courseProject.MappingProfile;
using AutoMapper;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
namespace courseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddDbContext<AppContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);

            //});
            builder.Services.AddControllers();
            builder.Services.AddDbContext<projectDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            builder.Services.AddScoped(typeof(IGenericRepository1<>), typeof(GenericRepository1<>));
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(ISubAdminRepository), typeof(SubAdminRepository));
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddAutoMapper(typeof(MappingProfileForStudentsInformation));
            builder.Services.AddAutoMapper(typeof(MappingForCourseInformation));
            builder.Services.AddAutoMapper(typeof(MappingForEmployee));
            builder.Services.AddAutoMapper(typeof(MappingForEvents));


            var Key = builder.Configuration.GetValue<string>("Authentication:SecretKey");

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(a =>
            {
                a.RequireHttpsMetadata = false;
                a.SaveToken = true;
                a.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });



            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("subAdmin", policy => policy.RequireRole("subAdmin"));
                options.AddPolicy("Admin&subAdmin", policy =>
                {
                    policy.RequireAssertion(a =>

                        a.User.IsInRole("Admin") ||
                        a.User.IsInRole("subAdmin")

                    );

                });
                options.AddPolicy("Instructor", policy => policy.RequireRole("Instructor"));
                options.AddPolicy("Student", policy => policy.RequireRole("Student"));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}