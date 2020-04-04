using SimpulBlog.Domain.Entities.Abstract;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class UserSocialMedia : Entity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long SocialMediaId { get; set; }
        public virtual SocialMedia SocialMedia { get; set; }
    }
}
