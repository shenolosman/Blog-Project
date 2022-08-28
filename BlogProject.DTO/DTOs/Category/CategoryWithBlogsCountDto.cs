namespace BlogProject.DTO.DTOs.Category
{
    public class CategoryWithBlogsCountDto
    {
        public int BlogsCount { get; set; }
        public Entities.Concrete.Category Category { get; set; }
    }
}
