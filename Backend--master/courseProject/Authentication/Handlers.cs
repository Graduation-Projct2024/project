using courseProject.Repository.Data;

namespace courseProject.Authentication
{
    public class Handlers
    {
      //  private readonly projectDbContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
      //  private readonly IServiceScopeFactory scopeFactory;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly projectDbContext context1;

        public Handlers(projectDbContext context, IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            
            context1 = context;
            _httpContextAccessor = httpContextAccessor;
            this.scopeFactory = scopeFactory;
            EnrolledInCourseHandler enrolledInCourseHandler = new EnrolledInCourseHandler(context, httpContextAccessor, scopeFactory);
        }
    }
}
