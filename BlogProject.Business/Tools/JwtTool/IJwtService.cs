using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Tools.JwtTool
{
    public interface IJwtService
    {
        JwtToken GenerateJwt(AppUser appUser);
    }
}
