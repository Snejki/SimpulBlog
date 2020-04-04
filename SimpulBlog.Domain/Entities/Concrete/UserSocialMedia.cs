using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class UserSocialMedia
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long SocialMediaId { get; set; }
        public virtual SocialMedia SocialMedia { get; set; }
    }
}
