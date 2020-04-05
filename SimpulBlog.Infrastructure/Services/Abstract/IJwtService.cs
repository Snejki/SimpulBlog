namespace SimpulBlog.Infrastructure.Services.Abstract
{
    public interface IJwtService : IService
    {
        string CreateToken(long userId);
    }
}
