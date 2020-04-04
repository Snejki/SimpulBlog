using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
