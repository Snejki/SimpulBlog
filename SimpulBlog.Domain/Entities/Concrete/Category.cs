using System.Collections;
using System.Collections.Generic;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class Category
    {
        public long Id { get; set; }
        public long Name { get; set; }

        public ICollection<ArticleCategory> ArticleCategories{ get; set; }
    }
}
