

using System.ComponentModel.DataAnnotations;

namespace BlogProject.Web.Models
{
    public class BlogAddModel
    {
        [Required(ErrorMessage = "Please fill the field!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please fill the field!")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Please fill the field!")]
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int AppUserId { get; set; }
    }
}
