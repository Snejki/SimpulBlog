using SimpulBlog.Domain.Entities.Abstract;
using System;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleView : Entity
    {
        public DateTime AddedAt { get; set; }

        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
