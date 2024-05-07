using courseProject.Core.IGenericRepository;
using courseProject.MappingProfile;
using courseProject.Repository.Data;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace courseProject.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository1<>), typeof(GenericRepository1<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
          //  services.AddScoped(typeof(ISubAdminRepository), typeof(SubAdminRepository));
          //  services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
          //  services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddAutoMapper(typeof(MappingProfileForStudentsInformation));
            services.AddAutoMapper(typeof(MappingForCourseInformation));
            services.AddAutoMapper(typeof(MappingForEmployee));
            services.AddAutoMapper(typeof(MappingForEvents));


            return services;
        }

        public static IServiceCollection AddInfrastucture (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<projectDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                         options.SerializerSettings.ReferenceLoopHandling =
                                 Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services , IConfiguration configuration)
        {
            var Key = configuration.GetValue<string>("Authentication:SecretKey");

            services.AddAuthentication(x =>
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

            return services;
        }
    }
}
