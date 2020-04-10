using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpulBlog.Core.Enums;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Core.Extensions;
using SimpulBlog.Core.Helpers;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Commands.UserArticleCommands;
using SimpulBlog.Infrastructure.Services.Abstract;

namespace SimpulBlog.Infrastructure.Services.Concrete
{
    public class UserArticleService : IUserArticleService
    {
        private readonly IUserRepository userRepository;
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICommitRepository<Article> commitRepository;
        private readonly IFileService fileService;

        private User user;
        private Article article;

        public UserArticleService(IUserRepository userRepository, IArticleRepository articleRepository, ICategoryRepository categoryRepository, ICommitRepository<Article> commitRepository, IFileService fileService)
        {
            this.userRepository = userRepository;
            this.articleRepository = articleRepository;
            this.categoryRepository = categoryRepository;
            this.commitRepository = commitRepository;
            this.fileService = fileService;
        }

        public async Task AddArticle(AddArticleCommand command)
        {
            await CheckIfUserExistAndCanAddArticle(command.UserId);
            await PrepareArticleToAdd(command);
            await AddArticle();
        }

        public async Task EditArticle(EditArticleCommand command)
        {
            await CheckIfUserCanEditorDeleteArticle(command.UserId, command.ArticleId);
            await PrepareArticleToEdit(command);
            await UpdateArticle();
        }

        public async Task DeleteArticle(DeleteArticleCommand command)
        {
            await CheckIfUserCanEditorDeleteArticle(command.UserId, command.ArticleId);
            PrepareArticleToRemove();
            await RemoveArticle();
        }

        private async Task CheckIfUserExistAndCanAddArticle(long userId)
        {
            user = await userRepository.GetById(userId);
            if (!user.IsActive)
                throw new BlogException(ErrorCode.NotFound);
        }

        private async Task CheckIfUserCanEditorDeleteArticle(long userId, long articleId)
        {
            await CheckIfUserExistAndCanAddArticle(userId);
            article = await articleRepository.GetById(articleId);
            if(user.UserRole != UserRole.Administrator && user.Id != article.UserId)
                throw new BlogException(ErrorCode.NoPermission);
        }

        private async Task PrepareArticleToAdd(AddArticleCommand command)
        {
            var articleCategories = await PrepareArticleCategories(command.ArticleCategories);
            var tags = PrepareTags(command.Tags);
            var addedAt = DateTimeHelpers.GetCurrenTime();
            var publishAt = PreparePublishAtDate(command.PublishAt, addedAt);
            var slug = PrepareSlug(command.Title, publishAt);
            var photoName = await fileService.UploadImage(command.TitlePhotoFile);

            article = new Article(
                command.Title,
                photoName,
                slug,
                command.ShortText,
                command.ArticleText,
                command.ReadTime,
                addedAt,
                publishAt,
                command.UserId,
                tags,
                articleCategories);
        }

        private async Task PrepareArticleToEdit(EditArticleCommand command)
        {
            var articleCategories = await PrepareArticleCategories(command.ArticleCategories);
            var tags = PrepareTags(command.Tags);
            var lastModifiedAt = DateTimeHelpers.GetCurrenTime();
            var publishAt = PreparePublishAtDate(command.PublishAt, lastModifiedAt);
            var slug = PrepareSlug(command.Title, publishAt);
            var photoName = await fileService.UploadImage(command.TitlePhotoFile);

            article.EditArticle(
                command.Title,
                photoName,
                slug,
                command.ShortText,
                command.ArticleText,
                command.ReadTime,
                lastModifiedAt,
                publishAt,
                tags,
                articleCategories);
        }

        private void PrepareArticleToRemove()
        {
            var deletedAt = DateTimeHelpers.GetCurrenTime();
            article.Delete(deletedAt);
        }

        private async Task AddArticle()
        {
            await commitRepository.Add(article);
            await commitRepository.Commit();
        }

        private async Task UpdateArticle()
        {
            await commitRepository.Update(article);
            await commitRepository.Commit();
        }

        private async Task RemoveArticle()
        {
            await commitRepository.Remove(article);
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
