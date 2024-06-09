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
using FluentValidation.AspNetCore;
using System.Reflection;
using FluentValidation;
using courseProject.Common;
using courseProject.Core.Models;
using Microsoft.AspNetCore.Identity;
using courseProject.Emails;
using courseProject.Services.Courses;
using courseProject.Services.BackgroundServices;


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




       //sieve     builder.Services.AddScoped<SieveProcessor>();

            builder.Services
                   .AddApplication()
                   .AddServices()
             //sieve      .AddSieve(builder.Configuration)
                   // .AddIdentities()
                   .AddInfrastucture(builder.Configuration)
                   .AddAuthenticationAndAuthorization(builder.Configuration);

            builder.Services.AddHostedService<DailyCheckBackgroundService>();
            //builder.Services.AddSingleton<IHostedService, CourseStatusUpdater>();

            //builder.Services.AddHostedService<CourseStatusUpdater>();


            builder. Services.AddEmailInfrastucture (builder. Configuration);
            builder.Services.AddHttpClient();


          

            //validations 
            //builder.Services.AddFluentValidation(
            //    v =>
            //    {
            //        v.ImplicitlyValidateChildProperties = true;
            //        v.ImplicitlyValidateRootCollectionElements = true;
            //        v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //    }            
            //    );
            builder.Services.AddFluentValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();


            builder.Services.AddHttpContextAccessor();
         

            builder.Services.AddScoped<IAuthorizationHandler, GetMaterialForEnrolledCourseHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, EnrolledInCourseHandler >();

           
          
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