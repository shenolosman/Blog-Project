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
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/Blogs/"); // from api applicationUrl in launchSettings
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

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var responseMsg = await _httpClient.GetAsync($"{id}");
            if (responseMsg.IsSuccessStatusCode)
            {
                JsonConvert.DeserializeObject<BlogListModel>(await responseMsg.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<List<BlogListModel>> GetAllByCategoryId(int id)
        {
            var msg = await _httpClient.GetAsync($"api/Blogs/GetAllByCategoryId/{id}");
            if (msg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await msg.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}
