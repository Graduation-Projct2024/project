using courseProject.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
namespace courseProject.Authentication.EnrolledInCourse
{
    public class EnrolledInCourseHandler : AuthorizationHandler<EnrolledInCourseRequirement>
    {
        private readonly projectDbContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        // private readonly IServiceScopeFactory context;
        // private readonly ILogger logger;


        public EnrolledInCourseHandler(projectDbContext context, IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            // this.context = scopeFactory;
            //  this.logger = logger;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EnrolledInCourseRequirement requirement)
        {
            //using (var scope = this.context.CreateScope())
            //{
            var httpContext = _httpContextAccessor.HttpContext;
            var routeData = httpContext.GetRouteData();
            var courseIdAsString = httpContext.Request.Query["CourseId"].FirstOrDefault()
                                   ?? routeData?.Values["CourseId"]?.ToString();
            if (!string.IsNullOrEmpty(courseIdAsString) && Guid.TryParse(courseIdAsString, out var courseId))
            {
                var userId = context.User.FindFirst("UserId")?.Value;
                if (userId != null)
                {
                    var enrolled = await dbContext.studentCourses.AnyAsync(sc => sc.courseId == courseId &&  (sc.StudentId.ToString() == userId || sc.Course.InstructorId.ToString() == userId));
                    if (enrolled)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {

                        context.Fail();
                    }


                }
            }
            //}
        }
    }
}

