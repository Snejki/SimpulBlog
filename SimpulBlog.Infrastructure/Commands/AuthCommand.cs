using System.Text.Json.Serialization;

namespace SimpulBlog.Infrastructure.Commands
{
    public class AuthCommand
    {
        [JsonIgnore]
        public long UserId { get; set; }
    }
}
