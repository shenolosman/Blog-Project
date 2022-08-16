using BlogProject.DTO.Interface;

namespace BlogProject.DTO.DTOs.AppUser
{
    public class AppUserLoginDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
