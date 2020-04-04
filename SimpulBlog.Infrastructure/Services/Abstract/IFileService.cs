using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpulBlog.Infrastructure.Services.Abstract
{
    public interface IFileService : IService
    {
        Task<string> Upload();
    }
}
