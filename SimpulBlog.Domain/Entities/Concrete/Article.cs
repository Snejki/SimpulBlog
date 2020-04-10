using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using SimpulBlog.Core.Exceptions;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class Article : Entity, IDeleteAble
    {
        public string Title { get; set; }
        public string TitlePhotoName { get; set; }
        public string Slug { get; set; }
        public string ShortText { get; set; }
        public string ArticleText { get; set; }
        public int ReadTime { get; set; }

        public DateTime AddedAt { get; set; }
        public DateTime PublishAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ArticleView> ArticleViews { get; set; }

        protected Article()
        {

        }

        public Article(
            string title, 
            string titlePhotoName, 
            string slug, 
            string shortText, 
            string articleText, 
            int readTime, 
            DateTime addedAt, 
            DateTime publishAt, 
            long userId, 
            ICollection<Tag> tags, 
            ICollection<ArticleCategory> articleCategories)
        {
            SetTitle(title);
            SetTitlePhotoName(titlePhotoName);
            SetSlug(slug);
            SetShortText(shortText);
            SetArticleText(articleText);
            SetReadTime(readTime);
            SetAddedAt(addedAt);
            SetPublishDate(publishAt);
            SetUserId(userId);
            SetTags(tags);
            SetArticleCategories(articleCategories);
        }

        public void EditArticle(
            string title,
            string titlePhotoName,
            string slug,
            string shortText,
            string articleText,
            int readTime,
            DateTime publishAt,
            DateTime lastModifiedAt,
            ICollection<Tag> tags,
            ICollection<ArticleCategory> articleCategories)
        {
            SetTitle(title);
            SetTitlePhotoName(titlePhotoName);
            SetSlug(slug);
            SetShortText(shortText);
            SetArticleText(articleText);
            SetReadTime(readTime);
            SetPublishDate(publishAt);
            SetTags(tags);
            SetArticleCategories(articleCategories);
            SetLastModifiedAt(lastModifiedAt);
        }

        public void Delete(DateTime deletedAt)
        {
            SetDeletedAt(deletedAt);
        }

        private void SetTitle(string title)
        {
            if(string.IsNullOrEmpty(title))
                throw new BlogException(ErrorCode.DomainValidationError, "Title can not be null");

            Title = title;
        }

        private void SetTitlePhotoName(string titlePhotoName)
        {
            TitlePhotoName = titlePhotoName;
        }

        private void SetSlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                throw new BlogException(ErrorCode.DomainValidationError, "Slug can not be null");

            Slug = slug;
        }

        private void SetShortText(string shortText)
        {
            ShortText = shortText;
        }

        private void SetArticleText(string articleText)
        {
            if (string.IsNullOrEmpty(articleText))
                throw new BlogException(ErrorCode.DomainValidationError, "Article text can not be null");

            ArticleText = articleText;
        }

        private void SetReadTime(int readTime)
        {
            ReadTime = readTime;
        }

        private void SetAddedAt(DateTime addedAt)
        {
            if (addedAt > DateTime.UtcNow || addedAt == DateTime.MinValue)
                throw new BlogException(ErrorCode.EntityValidationException, "Date is wrong");

            AddedAt = addedAt;
        }

        private void SetLastModifiedAt(DateTime lastModifiedAt)
        {
            if (lastModifiedAt > DateTime.UtcNow || lastModifiedAt == DateTime.MinValue)
                throw new BlogException(ErrorCode.EntityValidationException, "Date is wrong");

            LastModifiedAt = lastModifiedAt;
        }

        private void SetPublishDate(DateTime publishDate)
        {
            if (publishDate > DateTime.UtcNow || publishDate == DateTime.MinValue)
                throw new BlogException(ErrorCode.EntityValidationException, "Date is wrong");

            PublishAt = publishDate;
        }

        private void SetDeletedAt(DateTime deletedAt)
        {
            if (deletedAt > DateTime.UtcNow || deletedAt == DateTime.MinValue)
                throw new BlogException(ErrorCode.EntityValidationException, "Date is wrong");

            DeletedAt = deletedAt;
        }

        private void SetUserId(long userId)
        {
            UserId = userId;
        }

        private void SetTags(ICollection<Tag> tags)
        {
            Tags = tags;
        }

        private void SetArticleCategories(ICollection<ArticleCategory> articleCategories)
        {
            if(articleCategories == null || !articleCategories.Any())
                throw new BlogException(ErrorCode.EntityValidationException, "Article must have at least one category");

            ArticleCategories = articleCategories;
        }
    }
}
