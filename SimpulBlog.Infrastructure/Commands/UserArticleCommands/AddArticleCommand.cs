using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace SimpulBlog.Infrastructure.Commands.UserArticleCommands
{
    public class AddArticleCommand : AuthCommand, IRequest
    {
        public string Title { get; set; }
        public IFormFile TitlePhotoFile { get; set; }
        public string ShortText { get; set; }
        public string ArticleText { get; set; }
        public int ReadTime { get; set; }

        public DateTime? PublishAt { get; set; }
        public ICollection<string> Tags { get; set; }
        public virtual ICollection<long> ArticleCategories { get; set; }
    }
}
