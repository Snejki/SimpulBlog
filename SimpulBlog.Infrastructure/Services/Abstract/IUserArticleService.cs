using System.Threading.Tasks;
using SimpulBlog.Infrastructure.Commands.UserArticleCommands;

namespace SimpulBlog.Infrastructure.Services.Abstract
{
    public interface IUserArticleService : IService
    {
        Task AddArticle(AddArticleCommand command);
        Task EditArticle(EditArticleCommand command);
        Task DeleteArticle(DeleteArticleCommand command);
    }
}
