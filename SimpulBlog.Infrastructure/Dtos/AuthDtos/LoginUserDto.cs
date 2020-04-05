namespace SimpulBlog.Infrastructure.Dtos.AuthDtos
{
    public class LoginUserDto
    {
        public string Token { get; set; }

        public LoginUserDto()
        {

        }

        public LoginUserDto(string token)
        {
            Token = token;
        }
    }
}
