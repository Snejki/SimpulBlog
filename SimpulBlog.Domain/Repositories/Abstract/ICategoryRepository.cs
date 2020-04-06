using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpulBlog.Domain.Entities.Concrete;

namespace SimpulBlog.Domain.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository
    {
        Task<Category> GetById(long id);
    }
}
