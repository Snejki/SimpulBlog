using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Core.Extensions;
using SimpulBlog.Core.Helpers;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Commands.UserArticleCommands;
using SimpulBlog.Infrastructure.Services.Abstract;

namespace SimpulBlog.Infrastructure.Handlers.UserArticleHandlers
{
    public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICommitRepository<Article> commitRepository;
        private readonly IFileService fileService;

        private User user;
        private Article article;

        public AddArticleCommandHandler(ICommitRepository<Article> commitRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IFileService fileService)
        {
            this.commitRepository = commitRepository;
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
            this.fileService = fileService;
        }

        public async Task<Unit> Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            await CheckIfUserExistAndCanAddArticle(request.UserId);
            await PrepareArticle(request);
            await AddArticle();

            return Unit.Value;;
        }

        private async Task CheckIfUserExistAndCanAddArticle(long userId)
        {
            user = await userRepository.GetById(userId);
            if(!user.IsActive)
                throw new BlogException(ErrorCode.NotFound);
        }

        private async Task PrepareArticle(AddArticleCommand request)
        {
            var articleCategories = await PrepareArticleCategories(request.ArticleCategories);
            var tags = PrepareTags(request.Tags);
            var addedAt = DateTimeHelpers.GetCurrenTime();
            var publishAt = PreparePublishAtDate(request.PublishAt, addedAt);
            var slug = PrepareSlug(request.Title, publishAt);
            var photoName = await fileService.UploadImage(request.TitlePhotoFile);

            article = new Article(
                request.Title,
                photoName, 
                slug,
                request.ShortText, 
                request.ArticleText, 
                request.ReadTime, 
                addedAt,
                publishAt,
                request.UserId, 
                tags,
                articleCategories);
        }

        private async Task AddArticle()
        {
            await commitRepository.Add(article);
            await commitRepository.Commit();
        }

        private async Task<ICollection<ArticleCategory>> PrepareArticleCategories(ICollection<long> categoryIds)
        {
            var articleCategories = new List<ArticleCategory>();
            foreach (var id in categoryIds)
            {
                await categoryRepository.GetById(id);
                articleCategories.Add(new ArticleCategory(id));
            }

            return articleCategories;
        }

        private ICollection<Tag> PrepareTags(ICollection<string> tagsAsString)
        {
            var tags = new List<Tag>();
            foreach (var tag in tagsAsString)
            {
                tags.Add(new Tag(tag));
            }

            return tags;
        }

        private DateTime PreparePublishAtDate(DateTime? publishDate, DateTime addedAt)
        {
            if (publishDate.HasValue)
                return publishDate.Value;

            return addedAt;
        }

        private string PrepareSlug(string title, DateTime publishAt)
        {
            var text = $"{title}-{publishAt:yy-MM-dd}";
            return text.GenerateSlug();
        }
    }
}
