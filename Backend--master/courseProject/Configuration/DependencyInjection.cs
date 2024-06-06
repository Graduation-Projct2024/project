using courseProject.Authentication;
using courseProject.Authentication.EnrolledInCourse;
using courseProject.Authentication.MaterialInEnrolledCourse;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Emails;
using courseProject.MappingProfile;
using courseProject.Repository.Data;
using courseProject.Repository.GenericRepository;
using courseProject.Services.Courses;
using courseProject.Services.CourseStatus;
using courseProject.Services.Skill;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
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
          //  services.AddTransient<IEmailService, EmailService>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
           
            //  services.AddScoped(typeof(ISubAdminRepository), typeof(SubAdminRepository));
            //  services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            //  services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddAutoMapper(typeof(MappingForStudents));
            services.AddAutoMapper(typeof(MappingForCourse));
            services.AddAutoMapper(typeof(MappingForEmployee));
            services.AddAutoMapper(typeof(MappingForEvents));


            return services;
        }

        public static IServiceCollection AddInfrastucture (this IServiceCollection services , IConfiguration configuration)
        {
            //AuthenticationScheme.AddJwtAuthentication(services, configuration);

            services.AddDbContext<projectDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                         options.SerializerSettings.ReferenceLoopHandling =
                                 Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMemoryCache();



            //update status to start
          
            
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


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
                options.AddPolicy("subAdmin", policy => policy.RequireRole("subadmin"));
                
                options.AddPolicy("Instructor", policy => policy.RequireRole("instructor"));
                options.AddPolicy("Student", policy => policy.RequireRole("student"));
                options.AddPolicy("MainSubAdmin", policy => policy.RequireRole("main-subadmin"));
                options.AddPolicy("SubAdmin , Main-SubAdmin", policy => policy.RequireRole("subadmin", "main-subadmin"));
                options.AddPolicy("Admin&subAdmin", policy =>
                {
                    policy.RequireAssertion(a =>

                        a.User.IsInRole("admin") ||
                        a.User.IsInRole("subadmin")

                    );

                });
                options.AddPolicy("Main-SubAdmin , SubAdmin", policy =>
                {
                    policy.RequireAssertion(a =>


                        a.User.IsInRole("subadmin") ||
                        a.User.IsInRole("main-subadmin")

                    );

                });
                options.AddPolicy("Admin, Main-SubAdmin , SubAdmin", policy =>
                {
                    policy.RequireAssertion(a =>

                        a.User.IsInRole("admin") ||
                        a.User.IsInRole("subadmin")||
                        a.User.IsInRole("main-subadmin")

                    );

                });
                options.AddPolicy("Admin , Instructor", policy =>
                {
                    policy.RequireAssertion(a =>

                    a.User.IsInRole("admin") ||
                    a.User.IsInRole("instructor"));
                });

                options.AddPolicy("Admin , Student", policy =>
                {
                    policy.RequireAssertion(a =>

                    a.User.IsInRole("admin") ||
                    a.User.IsInRole("student"));
                });


                options.AddPolicy("Main-SubAdmin , Student", policy =>
                {
                    policy.RequireAssertion(a =>
                    a.User.IsInRole("main-subadmin") ||
                    a.User.IsInRole("student"));
                });

                options.AddPolicy("Main-SubAdmin ,Instructor , Student", policy =>
                {
                    policy.RequireAssertion(a =>
                    a.User.IsInRole("main-subadmin") ||
                    a.User.IsInRole("instructor") ||
                    a.User.IsInRole("student"));
                }
               );



                options.AddPolicy("EnrolledInCourse", policy =>
                policy.Requirements.Add(new EnrolledInCourseRequirement()));

                options.AddPolicy("MaterialInEnrolledCourse", policy =>
                policy.Requirements.Add(new MaterialInEnrolledCourseRequeriment()));

                options.AddPolicy("InstructorGiveTheCourse", policy =>
                {
                    policy.Requirements.Add(new EnrolledInCourseRequirement());
                    policy.RequireRole("instructor");
                }
              
                
                );

                options.AddPolicy("InstructorwhoGiveTheMaterial", policy =>
                {
                    policy.Requirements.Add(new MaterialInEnrolledCourseRequeriment());
                    policy.RequireRole("instructor");
                }
                );
                
                options.AddPolicy("MaterialInEnrolledCourseForStudent", policy =>
                {
                    policy.Requirements.Add(new MaterialInEnrolledCourseRequeriment());
                    policy.RequireRole("student");
                }
                );








            });

            return services;
        }






       // public static IServiceCollection AddIdentities(this IServiceCollection services)
       // {

       //     services.AddIdentity<User, IdentityRole>()
       //.AddEntityFrameworkStores<projectDbContext>()
       //.AddDefaultTokenProviders();



       //     services.AddIdentityCore<User>()
       //          .AddRoles<IdentityRole>()
       //       // .addIdentity<User , IdentityRole>
       //       //   .AddEntityFrameworkStores<projectDbContext>()
       //          .AddDefaultTokenProviders()
       //          .AddSignInManager<SignInManager<User>>();

       //     services.Configure<IdentityOptions>(options =>
       //     {
       //         options.Password.RequireDigit = false;
       //         options.Password.RequireLowercase = false;
       //         options.Password.RequireNonAlphanumeric = false;
       //         options.Password.RequireUppercase = false;
       //         options.Password.RequiredLength = 8;
       //         options.Password.RequiredUniqueChars = 1;
       //     }
       //     );

       //     return services;
       // }
    }
}
