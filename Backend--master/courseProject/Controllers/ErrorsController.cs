using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet("/Home/Error")]
        public IActionResult Error()
        {
            // Get the exception details, if available
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;

            return Problem(detail: exception?.Message);
        }
    }
}
