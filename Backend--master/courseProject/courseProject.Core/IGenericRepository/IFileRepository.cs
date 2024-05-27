using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IFileRepository
    {
        public  Task<string> UploadFile1(IFormFile file);
        public Task<string> UploadImage(IFormFile file);
        public Task<List<string>> UploadFilesAsync(List<IFormFile> files);
    }
}
