using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Core.Extensions;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class Tag : Entity
    {
        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public string Name { get; set; }

        public Tag()
        {
            
        }

        public Tag(string name)
        {
            if(string.IsNullOrEmpty(name) || !name.IsTag())
                throw new BlogException(ErrorCode.EntityValidationException);

            Name = name;
        }
    }
}
