using BlogProject.DTO.Interface;

namespace BlogProject.DTO.DTOs.Category
{
    public class CategoryListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
