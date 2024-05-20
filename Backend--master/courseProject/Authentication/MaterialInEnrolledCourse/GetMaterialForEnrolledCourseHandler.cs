using courseProject.Authentication.EnrolledInCourse;
using courseProject.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace courseProject.Authentication.MaterialInEnrolledCourse
{
    public class GetMaterialForEnrolledCourseHandler : AuthorizationHandler<MaterialInEnrolledCourseRequeriment>
    {
        private readonly projectDbContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory context;
        public GetMaterialForEnrolledCourseHandler(projectDbContext context, IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            this.context = scopeFactory;

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MaterialInEnrolledCourseRequeriment requirement)
        {
            //using (var scope = this.context.CreateScope())
            //{
            var httpContext = _httpContextAccessor.HttpContext;
            var routeData = httpContext.GetRouteData();
            var MaterialIdAsString = httpContext.Request.Query["Id"].FirstOrDefault()
                                   ?? routeData?.Values["Id"]?.ToString();
            if (!string.IsNullOrEmpty(MaterialIdAsString) && int.TryParse(MaterialIdAsString, out var MaterialId))
            {
                var courseid = (await dbContext.courseMaterials.FirstOrDefaultAsync(x => x.Id == MaterialId))?.courseId;
                var userId = context.User.FindFirst("UserId")?.Value;
                if (userId != null)
                {
                    var enrolled = await dbContext.studentCourses.AnyAsync(sc => sc.courseId == courseid && (sc.StudentId.ToString() == userId || sc.Course.InstructorId.ToString() == userId));
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

