using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.LogTool;
using BlogProject.DTO.DTOs.Category;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ICustomLogger _customLogger;

        public CategoryController(IMapper mapper, ICategoryService categoryService, ICustomLogger customLogger)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _customLogger = customLogger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllSortedByIdAsync()));
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<CategoryListDto>(await _categoryService.FindByIdAsync(id)));
        }
        [HttpPost]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Create(CategoryAddDto model)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(model));
            return Created("", model);
        }
        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto model)
        {
            if (id != model.Id)
            {
                return BadRequest("Id does not match");
            }
            await _categoryService.UpdateAsync(_mapper.Map<Category>(model));
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(await _categoryService.FindByIdAsync(id));
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithBlogsCount()
        {
            var categories = await _categoryService.GetAllWithCategoryBlogsAsync();
            var listCategory = new List<CategoryWithBlogsCountDto>();
            foreach (var category in categories)
            {
                var dto = new CategoryWithBlogsCountDto
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    BlogsCount = category.CategoryBlogs.Count
                };

                listCategory.Add(dto);
            }
            return Ok(listCategory);
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.LogError($"\nError place: {errorInfo.Path}\n Error message: {errorInfo.Error.Message}\n Stack trace: {errorInfo.Error.StackTrace}");
            return Problem(detail: "Please contact with developers.");
        }
    }
}
