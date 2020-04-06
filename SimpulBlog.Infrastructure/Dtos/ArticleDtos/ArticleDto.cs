using System;
using SimpulBlog.Domain.Entities.Concrete;

namespace SimpulBlog.Infrastructure.Dtos.ArticleDtos
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string TitlePhotoname { get; set; }
        public string ShortText { get; set; }
        public int ReadTime { get; set; }
        public int CommentsCount { get; set; }

        public DateTime PublishAt { get; set; }

        public string Author { get; set; }
    }
}
