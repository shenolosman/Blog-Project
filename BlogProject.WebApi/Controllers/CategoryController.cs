using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.DTO.DTOs.Category;
using BlogProject.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllSortedByIdAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<CategoryListDto>(await _categoryService.FindByIdAsync(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddDto model)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(model));
            return Created("", model);
        }
        [HttpPut("{id}")]
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
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(new Category() { Id = id });
            return NoContent();
        }
    }
}
