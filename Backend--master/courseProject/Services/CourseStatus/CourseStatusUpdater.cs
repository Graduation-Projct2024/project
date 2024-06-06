using courseProject.Core.IGenericRepository;
using courseProject.Services.Courses;
using System.Reflection;

namespace courseProject.Services.CourseStatus
{
    public class CourseStatusUpdater : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;


        public CourseStatusUpdater(IServiceScopeFactory scopeFactory )
        {
            _scopeFactory = scopeFactory;
     
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope()) // Use regular scope for simplicity
                {
                    await UpdateCourseStatusesAsync(scope.ServiceProvider);
                }
                // Wait for 24 hours before running again
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }


        private async Task UpdateCourseStatusesAsync(IServiceProvider serviceProvider)
        {
            var courseService = serviceProvider.GetService<ICourseServices>();
            var unitwork = serviceProvider.GetRequiredService<IUnitOfWork>();

            var courses =  courseService.GetAllCourses().Result;
            var coursee = unitwork.CourseRepository.GetAllCoursesAsync().Result;

                foreach (var course in courses)
                {

                    if (course.startDate.Value.Date == DateTime.UtcNow.Date && course.status != "start")
                    {
                        course.status = "start";
                    await courseService.accreditCourse(course.Id, "start") ;
                    }
                }

            
    }
    }
}
