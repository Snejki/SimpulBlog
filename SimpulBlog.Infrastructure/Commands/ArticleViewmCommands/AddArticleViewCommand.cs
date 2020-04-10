using MediatR;

namespace SimpulBlog.Infrastructure.Commands.ArticleViewmCommands
{
    public class AddArticleViewCommand : IRequest
    {
        public long ArticleId { get; set; }

        public AddArticleViewCommand()
        {
            
        }

        public AddArticleViewCommand(long articleId)
        {
            ArticleId = articleId;
        }
    }
}
