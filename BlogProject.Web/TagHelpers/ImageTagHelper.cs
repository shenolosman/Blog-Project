using BlogProject.Web.ApiServices.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogProject.Web.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class ImageTagHelper : TagHelper
    {
        private readonly IImageApiService _imageApiService;
        public int Id { get; set; }

        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var blob = await _imageApiService.GetBlogImageByIdAsync(Id);
            var html = $"<img src='{blob}' class='card-img-top' alt='{blob}'> ";
            output.Content.SetHtmlContent(html);
        }
    }
}
