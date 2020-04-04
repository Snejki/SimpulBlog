using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class Comment : Entity, IDeleteAble
    {
        public string Text { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }

        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
