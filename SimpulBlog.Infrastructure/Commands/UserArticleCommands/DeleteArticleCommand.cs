using MediatR;

namespace SimpulBlog.Infrastructure.Commands.UserArticleCommands
{
    public class DeleteArticleCommand : AuthCommand, IRequest
    {
        public long ArticleId { get; set; }

        public DeleteArticleCommand()
        {
            
        }

        public DeleteArticleCommand(long articleId)
        {
            ArticleId = articleId;
        }
    }
}
