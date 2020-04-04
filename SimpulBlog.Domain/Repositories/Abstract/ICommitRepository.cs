using SimpulBlog.Domain.Entities.Abstract;
using System.Threading.Tasks;

namespace SimpulBlog.Domain.Repositories.Abstract
{
    public interface ICommitRepository<T> where T : Entity
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);

        Task Commit();
    }
}
