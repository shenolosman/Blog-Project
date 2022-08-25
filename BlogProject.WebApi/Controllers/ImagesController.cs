using BlogProject.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IBlogService blogService;

        public ImagesController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetBlogImageById(int id)
        {
            var blog = await blogService.FindByIdAsync(id);
            if (string.IsNullOrWhiteSpace(blog.ImagePath))
            {
                return NotFound("No Image");
            }
            return File($"/img/{blog.ImagePath}", "image/jpeg");
        }
    }
}
