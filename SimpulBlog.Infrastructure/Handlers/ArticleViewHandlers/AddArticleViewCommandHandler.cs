using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpulBlog.Core.Helpers;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Commands.ArticleViewmCommands;

namespace SimpulBlog.Infrastructure.Handlers.ArticleViewHandlers
{
    public class AddArticleViewCommandHandler : IRequestHandler<AddArticleViewCommand>
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICommitRepository<ArticleView> commitRepository;

        private ArticleView articleView;

        public AddArticleViewCommandHandler(ICommitRepository<ArticleView> commitRepository, IArticleRepository articleRepository)
        {
            this.commitRepository = commitRepository;
            this.articleRepository = articleRepository;
        }

        public async Task<Unit> Handle(AddArticleViewCommand request, CancellationToken cancellationToken)
        {
            await CheckIfArticleExist(request.ArticleId);
            PrepareArticleVIew(request.ArticleId);
            await AddArticleView();

            return Unit.Value;
        }

        private async Task CheckIfArticleExist(long articleId)
        {
            await articleRepository.GetPublishedById(articleId);
        }

        private void PrepareArticleVIew(long articleId)
        {
            var addedAt = DateTimeHelpers.GetCurrenTime();
            articleView = new ArticleView(articleId, addedAt);
        }

        private async Task AddArticleView()
        {
            await commitRepository.Add(articleView);
            await commitRepository.Commit();
        }
    }
}
