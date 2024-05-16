using courseProject.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace courseProject.Authentication
{
    public class GiveTheCourseHandler : AuthorizationHandler<GiveTheCourseRequirements>
    {
        private readonly projectDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory context;
        public GiveTheCourseHandler(projectDbContext context, IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            this.context = scopeFactory;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GiveTheCourseRequirements requirement)
        {
            using (var scope = this.context.CreateScope())
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var routeData = httpContext.GetRouteData();
                var courseIdAsString = httpContext.Request.Query["CourseId"].FirstOrDefault<string>()
                                       ?? routeData?.Values["CourseId"]?.ToString();
                if (!string.IsNullOrEmpty(courseIdAsString) && int.TryParse(courseIdAsString, out var courseId))
                {
                    var userId = context.User.FindFirst("UserId")?.Value;                   
                        if (userId != null)
                    {
                        var enrolled = await _context.courses.AnyAsync(sc => sc.Id == courseId && sc.InstructorId.ToString() == userId);
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
            }
        }
    }
}

