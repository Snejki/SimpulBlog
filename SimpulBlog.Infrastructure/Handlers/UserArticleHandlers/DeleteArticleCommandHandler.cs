using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpulBlog.Infrastructure.Commands.UserArticleCommands;
using SimpulBlog.Infrastructure.Services.Abstract;

namespace SimpulBlog.Infrastructure.Handlers.UserArticleHandlers
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly IUserArticleService articleService;

        public DeleteArticleCommandHandler(IUserArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            await articleService.DeleteArticle(request);
            return Unit.Value;;
        }
    }
}
