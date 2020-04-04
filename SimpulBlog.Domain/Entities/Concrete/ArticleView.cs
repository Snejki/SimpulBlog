using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleView
    {
        public long Id { get; set; }
        public string UserIdentifier { get; set; }
        public DateTime AddedAt { get; set; }

        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
