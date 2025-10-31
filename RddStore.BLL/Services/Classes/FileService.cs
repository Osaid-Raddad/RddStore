using Microsoft.AspNetCore.Http;
using RddStore.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<string> UploadAsync(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", fileName);

                using (var stream = File.Create(filePath))
                {
                   await file.CopyToAsync(stream);
                }
                return fileName;
            }
            throw new Exception("File is empty or null");
        }
    }
}
