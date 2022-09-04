using System.ComponentModel.DataAnnotations;

namespace BlogProject.Web.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage = "Fill the field!")]
        public string Name { get; set; }
    }
}
