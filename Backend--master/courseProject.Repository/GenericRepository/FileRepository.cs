using Microsoft.AspNetCore.Http;
using courseProject.Core.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Schema;
using System.ComponentModel.DataAnnotations.Schema;


namespace courseProject.Repository.GenericRepository
{
    public class FileRepository : IFileRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadFile1(IFormFile file)
        {
            
            string fileName = "";
            var uploadPath = "";
            try
            {
                if (file == null || file.Length == 0)
                {
                    return null;
                }
                //var Id = Convert.ToString(httpContextAccessor.HttpContext.User.FindFirst("UserId"));
                //int.TryParse(Id, out int id);
                var extension = Path.GetExtension(file.FileName);
                fileName =fileName+"_" +DateTimeOffset.Now.Ticks ;
                fileName += extension;
                 uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var exactPath = Path.Combine(uploadPath, fileName);

                using (var fileStream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {

                fileName = $"Error: {ex.Message}";
            }
         
           
            return fileName;
        }


        public async Task<List<string>> UploadFiles(List<IFormFile> files)
        {
            var fileNames = new List<string>();
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\");

            try
            {
                if (files == null || files.Count == 0)
                {
                    return null;
                }

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        fileNames.Add("Error: File is empty or null");
                        continue;
                    }

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = DateTimeOffset.Now.Ticks.ToString() + extension;
                    var exactPath = Path.Combine(uploadPath, fileName);

                    using (var fileStream = new FileStream(exactPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    fileNames.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                fileNames.Add($"Error: {ex.Message}");
            }

            return fileNames;
        }


        public async Task<string> GetFileUrl(string fileName)
        {
            var scheme = httpContextAccessor.HttpContext?.Request.Scheme ?? string.Empty; // http or https
            var host = httpContextAccessor.HttpContext?.Request.Host; //  localhost:7116
            var pathBase = httpContextAccessor.HttpContext?.Request.PathBase ?? string.Empty;

            
            var fileUrl = $"{scheme}://{host}{pathBase}/{fileName}";

            return fileUrl;
        }

        public async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
        {
            var uploadedFileNames = new List<string>();

            try
            {
                if (files == null || files.Count == 0)
                {
                    return null;
                }

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in files)
                {
                    if (file.Length == 0)
                    {
                        continue;
                    }

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = DateTimeOffset.Now.Ticks.ToString() + extension;
                    var exactPath = Path.Combine(uploadPath, fileName);

                    using (var fileStream = new FileStream(exactPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    uploadedFileNames.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                // Optionally handle the exception (e.g., log it)
                uploadedFileNames.Add($"Error: {ex.Message}");
            }

            return uploadedFileNames;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            
            string fileName = "";
            var uploadPath = "";
            try
            {
                if (file == null || file.Length == 0)
                {
                    return null;
                }
                var extension = Path.GetExtension(file.FileName);
                //var allowdExtentions = new string[] {".png" , ".jpg" , ".jpeg" };
                //if(!allowdExtentions.Contains(extension))
                //{
                //    return $"The image extention ={extension} is not allowd";
                //}
                fileName = DateTimeOffset.Now.Ticks.ToString() + extension;

                 uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var exactPath = Path.Combine(uploadPath, fileName);

                using (var fileStream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {

                fileName = $"Error: {ex.Message}";
            }

            return fileName;
        }
    }
}
