using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.JwtTool;
using BlogProject.DTO.DTOs.AppUser;
using BlogProject.WebApi.CustomFilters;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IJwtService _jwtService;

        public AuthController(IAppUserService appUserService, IJwtService jwtService)
        {
            _appUserService = appUserService;
            _jwtService = jwtService;
        }
        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignIn(AppUserLoginDto appUserLoginDto)
        {
            var user = await _appUserService.CheckUserAsync(appUserLoginDto);
            if (user == null)
                return BadRequest("Username or password is false!");

            return Created("", _jwtService.GenerateJwt(user));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await _appUserService.FindByNameAsync(User.Identity.Name);
            return Ok(new AppUserDto { Id = user.Id, Name = user.Name, Surname = user.Surname });
        }
    }
}
