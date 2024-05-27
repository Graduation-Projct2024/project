using courseProject.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private projectDbContext dbContext;

        [HttpGet("DownloadFile")]
        [Authorize]
        public async Task<IActionResult> DownloadFile(string filename)
        {
           
            var filepath = Path.Combine(Directory.GetCurrentDirectory(),  filename);
            if(!System.IO.File.Exists(filepath))
            {
                return NotFound();
            }
            
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}
