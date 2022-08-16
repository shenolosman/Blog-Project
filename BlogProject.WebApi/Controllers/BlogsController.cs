using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.DTO.DTOs.Blog;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogsController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<BlogListDto>>(await _blogService.GelAllSortedByPostedTimeAsync()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<BlogListDto>(await _blogService.FinByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            await _blogService.AddAsync(_mapper.Map<Blog>(model));
            return Created("", model);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BlogUpdateModel model)
        {
            if (id != model.Id)
                return BadRequest("No valid id");
            await _blogService.UpdateAsync(_mapper.Map<Blog>(model));
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
