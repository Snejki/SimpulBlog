using System;
using System.Collections.Generic;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string TitlePhotoname { get; set; }
        public string Slug { get; set; }
        public string ShortText { get; set; }
        public string ArticleText { get; set; }
        public int Views { get; set; }
        public int ReadTime { get; set; }
        public int CommentsCount { get; set; }

        public DateTime AddedAt { get; set; }
        public DateTime PublishAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ArticleView> ArtivleViews { get; set; }
    }
}
