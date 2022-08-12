using BlogProject.Entities.Interfaces;

namespace BlogProject.Entities.Concrete
{
    public class AppUser : ITable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Blog> Blogs { get; set; }
        //For improve this project can add roles as editor, admin,member, etc.
    }
}
