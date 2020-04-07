using System;

namespace SimpulBlog.Infrastructure.Dtos.CommentDtos
{
    public class CommentDto
    {
        public string Text { get; set; }
        public DateTime AddedAt { get; set; }
        public string Name { get; set; }
    }
}
