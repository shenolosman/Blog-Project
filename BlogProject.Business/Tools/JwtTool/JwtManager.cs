using BlogProject.Business.StringInfos;
using BlogProject.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogProject.Business.Tools.JwtTool
{
    public class JwtManager : IJwtService
    {
        public JwtToken GenerateJwt(AppUser appUser)
        {
            SymmetricSecurityKey ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey));

            SigningCredentials sc = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jst = new JwtSecurityToken(issuer: JwtInfo.Issuer, audience: JwtInfo.Audience, claims: SetClaims(appUser), notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(JwtInfo.Expires), signingCredentials: sc);

            JwtToken token = new JwtToken();

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            token.Token = handler.WriteToken(jst);

            return token;
        }

        private List<Claim> SetClaims(AppUser appuser)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, appuser.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appuser.Id.ToString()));

            return claims;
        }
    }
}
