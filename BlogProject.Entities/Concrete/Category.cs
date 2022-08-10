using BlogProject.Entities.Interfaces;

namespace BlogProject.Entities.Concrete
{
    public class Category : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
