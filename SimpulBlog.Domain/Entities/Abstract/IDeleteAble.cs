using System;

namespace SimpulBlog.Domain.Entities.Abstract
{
    public interface IDeleteAble
    {
        public DateTime? DeletedAt { get; }
    }
}
