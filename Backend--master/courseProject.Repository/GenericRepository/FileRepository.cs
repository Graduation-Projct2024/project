using Microsoft.AspNetCore.Http;
using courseProject.Core.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class FileRepository : IFileRepository
    {
        public async Task<string> UploadFile1(IFormFile file)
        {
            
            string fileName = "";
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("File is empty or null.");
                }


                var extension = Path.GetExtension(file.FileName);
                fileName = file.FileName ;

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

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
