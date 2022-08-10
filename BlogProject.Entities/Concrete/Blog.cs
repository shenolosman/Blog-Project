using BlogProject.Entities.Interfaces;

namespace BlogProject.Entities.Concrete
{
    public class Blog : ITable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
