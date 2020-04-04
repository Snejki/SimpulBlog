using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class SocialMedia : Entity
    {
        public string Name { get; set; }
        public string IconName { get; set; }

        public virtual ICollection<UserSocialMedia> UserSocialMedia { get; set; }
    }
}
