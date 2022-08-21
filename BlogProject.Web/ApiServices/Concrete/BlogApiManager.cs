using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Models;
using Newtonsoft.Json;

namespace BlogProject.Web.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;

        public BlogApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5297/api/blogs/"); // from api applicationUrl in launchSettings
        }
        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseMsg = await _httpClient.GetAsync("");
            if (responseMsg.IsSuccessStatusCode)
            {
                JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMsg.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
