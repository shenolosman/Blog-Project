using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.DTO.DTOs.Blog;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.Enums;
using BlogProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
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
            return Ok(_mapper.Map<BlogListDto>(await _blogService.FindByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogAddModel model)
        {
            var uploadModel = await UploadFileAsync(model.Image, "image/jpeg");

            if (uploadModel.UploadState == UploadState.Success)
            {
                model.ImagePath = uploadModel.NewName;
                await _blogService.AddAsync(_mapper.Map<Blog>(model));
                return Created("", model);
            }
            else if (uploadModel.UploadState == UploadState.NotExist)
            {
                await _blogService.AddAsync(_mapper.Map<Blog>(model));
                return Created("", model);
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BlogUpdateModel model)
        {
            var uploadModel = await UploadFileAsync(model.Image, "image/jpeg");

            if (uploadModel.UploadState == UploadState.Success)
            {
                var updatedBlog = await _blogService.FindByIdAsync(model.Id);
                updatedBlog.ShortDescription = model.ImagePath;
                updatedBlog.Title = model.Title;
                updatedBlog.Description = model.Description;

                updatedBlog.ImagePath = uploadModel.NewName;

                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else if (uploadModel.UploadState == UploadState.NotExist)
            {
                var updatedBlog = await _blogService.FindByIdAsync(model.Id);
                updatedBlog.ShortDescription = model.ImagePath;
                updatedBlog.Title = model.Title;
                updatedBlog.Description = model.Description;

                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(new Blog() { Id = id });
            return NoContent();
        }
    }
}
