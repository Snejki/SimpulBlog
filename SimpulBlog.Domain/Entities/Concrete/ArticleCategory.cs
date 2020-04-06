using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleCategory : Entity
    {
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }

        protected ArticleCategory()
        {
            
        }

        public ArticleCategory(long categoryId)
        {
            SetCategoryId(categoryId);
        }

        private void SetCategoryId(long categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
