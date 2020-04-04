using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleTag
    {
        public long Id { get; set; }

        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
