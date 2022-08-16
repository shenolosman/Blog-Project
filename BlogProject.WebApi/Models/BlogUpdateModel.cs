namespace BlogProject.WebApi.Models
{
    public class BlogUpdateModel
    {
        //This model created because of IFormFile belongs to aspcore thats why this model not stay as a DTO in DTO project!
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public int AppUserId { get; set; }
    }
}
