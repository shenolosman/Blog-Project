namespace BlogProject.Web.ApiServices.Interfaces
{
    public interface IImageApiService
    {
        Task<string> GetBlogImageByIdAsync(int id);
    }
}
