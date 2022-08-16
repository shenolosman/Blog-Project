using BlogProject.Business.Interfaces;
using BlogProject.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _blogService.GelAllSortedByPostedTimeAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _blogService.FinByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog model)
        {
            await _blogService.AddAsync(model);
            return Created("", model);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Blog model)
        {
            if (id != model.Id)
                return BadRequest("No valid id");
            await _blogService.UpdateAsync(model);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(new Blog() { Id = id });
            return NoContent();
        }
    }
}
