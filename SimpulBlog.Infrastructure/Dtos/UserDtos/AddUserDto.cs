namespace SimpulBlog.Infrastructure.Dtos.UserDtos
{
    public class AddUserDto
    {
        public string Password { get; set; }

        public AddUserDto()
        {

        }

        public AddUserDto(string password)
        {
            Password = password;
        }
    }
}
