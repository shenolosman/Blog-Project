using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Models;
using Newtonsoft.Json;

namespace BlogProject.Web.ApiServices.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/Category/");
        }
        public async Task<List<CategoryListModel>> GetAllAsync()
        {
            var responseMsg = await _httpClient.GetAsync("");
            if (responseMsg.IsSuccessStatusCode)
            {
                JsonConvert.DeserializeObject<List<CategoryListModel>>(await responseMsg.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<List<CategoryWithBlogsCountModel>> GetAllWithBlogsCount()
        {
            var msg = await _httpClient.GetAsync("GetWithBlogsCount");
            if (msg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryWithBlogsCountModel>>(
                    await msg.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<CategoryListModel> GetByIdAsync(int id)
        {
            var msg = await _httpClient.GetAsync($"{id}");
            if (msg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryListModel>(await msg.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
