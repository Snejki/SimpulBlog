using SimpulBlog.Domain.Entities.Abstract;
using System.Collections.Generic;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class Category : Entity
    {
        public long Name { get; set; }

        public virtual ICollection<ArticleCategory> ArticleCategories{ get; set; }
    }
}
