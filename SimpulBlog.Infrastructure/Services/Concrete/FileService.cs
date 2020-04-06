using SimpulBlog.Infrastructure.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SimpulBlog.Infrastructure.Services.Concrete
{
    class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string uploadFolderName = "Uploads";

        public FileService(
            IWebHostEnvironment webHostEnvironment
        )
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile photoFile)
        {
            var fileExtension = Path.GetExtension(photoFile.FileName);
            var fileName = GetUniqueFileName(fileExtension);
            var uploadPath = GetUploadPath();
            var filePath = $"{uploadPath}/{fileName}";

            await photoFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

            return fileName;
        }


        private string GetUploadPath()
        {
            return Path.Combine(_webHostEnvironment.WebRootPath, uploadFolderName);
        }

        private string GetUniqueFileName(string fileExtension)
        {
            return $"{Guid.NewGuid():N}{fileExtension}";
        }
    }
}
