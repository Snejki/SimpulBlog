using System.Text.Json.Serialization;

namespace SimpulBlog.Infrastructure.Queries
{
    public class AuthQuery
    {
        [JsonIgnore]
        public long UserId { get; set; }
    }
}
