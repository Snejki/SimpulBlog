using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleCategory
    {
        public long Id { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
