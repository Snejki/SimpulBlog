using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleView : Entity
    {
        public string UserIdentifier { get; set; }
        public DateTime AddedAt { get; set; }

        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
