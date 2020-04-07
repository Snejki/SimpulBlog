using System;
using System.Collections.Generic;
using System.Text;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Infrastructure.Dtos.CommentDtos;
using SimpulBlog.Infrastructure.Dtos.TagDtos;

namespace SimpulBlog.Infrastructure.Dtos.ArticleDtos
{
    public class ArticleDetailsDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string TitlePhotoname { get; set; }
        public string Slug { get; set; }
        public string ShortText { get; set; }
        public string ArticleText { get; set; }
        public int ReadTime { get; set; }

        public DateTime PublishAt { get; set; }

        public long UserId { get; set; }
        public string Author { get; set; }

        public virtual ICollection<TagDto> Tags { get; set; }
        public virtual ICollection<CommentDto> Comments { get; set; }
    }
}
