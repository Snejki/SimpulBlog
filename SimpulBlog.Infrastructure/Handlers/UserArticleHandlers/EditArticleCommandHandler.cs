using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpulBlog.Infrastructure.Commands.UserArticleCommands;
using SimpulBlog.Infrastructure.Services.Abstract;

namespace SimpulBlog.Infrastructure.Handlers.UserArticleHandlers
{
    public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand>
    {
        private readonly IUserArticleService articleService;

        public EditArticleCommandHandler(IUserArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<Unit> Handle(EditArticleCommand request, CancellationToken cancellationToken)
        {
            await articleService.EditArticle(request);
            return Unit.Value;
        }
    }
}
