using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogProject.Web.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class ImageTagHelper : TagHelper
    {
        private readonly IImageApiService _imageApiService;
        public int Id { get; set; }
        public BlogImageType BlogImageType { get; set; } = BlogImageType.BlogHome;
        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var blob = await _imageApiService.GetBlogImageByIdAsync(Id);
            string html = string.Empty;
            if (BlogImageType == BlogImageType.BlogHome)
            {
                html = $"<img src='{blob}' class='card-img-top' alt='{blob}'> ";
            }
            else
            {
                html = $"<img src='{blob}' class='img-fluid rounded' alt='{blob}'> ";
            }

            output.Content.SetHtmlContent(html);
        }
    }
}
