using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.FacadeTool;
using BlogProject.DTO.DTOs.Category;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IFacade _facade;

        public CategoryController(IMapper mapper, ICategoryService categoryService, IFacade facade)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _facade = facade;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (_facade.MemoryCache.TryGetValue("catList", out List<CategoryListDto> list))
            {
                return Ok(list);
            }
            var catList = _mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllSortedByIdAsync());

            _facade.MemoryCache.Set("catList", catList, new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddDays(1), Priority = CacheItemPriority.Normal });
            return Ok(catList);
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


    }
}
