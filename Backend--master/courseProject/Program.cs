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
using Microsoft.Extensions.FileProviders;
using courseProject.Configuration;
using Microsoft.AspNetCore.Authorization;
using courseProject.Authentication;
using courseProject.Authentication.EnrolledInCourse;
using courseProject.Authentication.MaterialInEnrolledCourse;

namespace courseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

     



            builder.Services.AddCors(a =>
            {
                a.AddPolicy("AllowOrigin", policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost:3000");
                    
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowCredentials();
                });
            });

    


            builder.Services
                   .AddApplication()
                   .AddInfrastucture(builder.Configuration)
                   .AddAuthenticationAndAuthorization(builder.Configuration);


            builder.Services.AddHttpClient();

            
                //options.AddPolicy("Instructor", policy => policy.RequireRole("instructor"));
                //options.AddPolicy("Student", policy => policy.RequireRole("student"));
                //options.AddPolicy("MainSubAdmin", policy => policy.RequireRole("main-subadmin"));
                //options.AddPolicy("SubAdmin , Main-SubAdmin", policy=>policy.RequireRole("subadmin" , "main-subadmin"));
                //options.AddPolicy("EnrolledInCourse", policy =>
                //{
                //    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                //    policy.RequireAuthenticatedUser();
                //    policy.Requirements.Add(new InstructorGivenCourseRequirement());
                //});
                //options.AddPolicy("EnrolledInCourse", policy =>
                //policy.Requirements.Add(new InstructorGivenCourseRequirement()));

                //options.AddPolicy("MaterialInEnrolledCourse", policy =>
                //policy.Requirements.Add(new MaterialInEnrolledCourseRequeriment()));

                //options.AddPolicy("InstructorGiveTheCourse", policy =>
                //policy.Requirements.Add(new GiveTheCourseRequirements()));
            

            builder.Services.AddHttpContextAccessor();
          //  builder.Services.AddScoped<IAuthorizationHandler, CombinedHandler>();

            builder.Services.AddScoped<IAuthorizationHandler, GetMaterialForEnrolledCourseHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, EnrolledInCourseHandler >();

           
            //   builder.Services.AddScoped<IAuthorizationHandler, GiveTheCourseHandler>();
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
            
           
            app.UseCors("AllowOrigin");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/Files"
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}