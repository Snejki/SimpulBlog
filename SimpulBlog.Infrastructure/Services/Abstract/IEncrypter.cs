namespace SimpulBlog.Infrastructure.Services.Abstract
{
    public interface IEncrypter : IService
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
        void CompareHash(string dbHash, string requestHash);
    }
}
