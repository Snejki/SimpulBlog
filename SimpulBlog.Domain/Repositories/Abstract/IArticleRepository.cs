using System.Collections.Generic;
using System.Threading.Tasks;
using SimpulBlog.Domain.Entities.Concrete;

namespace SimpulBlog.Domain.Repositories.Abstract
{
    public interface IArticleRepository : IRepository
    {
        Task<ICollection<Article>> GetPublishedPage(int page, int pageSize, long categoryId);
        Task<int> GetPublishedPagesCount(int pageSize, long categoryId);
    }
}
