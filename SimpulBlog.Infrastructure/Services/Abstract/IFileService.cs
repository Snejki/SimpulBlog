using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SimpulBlog.Infrastructure.Services.Abstract
{
    public interface IFileService : IService
    {
        Task<string> UploadImage(IFormFile photoFile);
    }
}
