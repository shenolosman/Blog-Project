using BlogProject.Entities.Interfaces;

namespace BlogProject.Entities.Concrete
{
    public class CategoryBlog : ITable
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
