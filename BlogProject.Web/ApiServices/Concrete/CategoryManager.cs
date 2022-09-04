using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BlogProject.Web.ApiServices.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/Category/");
        }
        public async Task<List<CategoryListModel>> GetAllAsync()
        {
            var responseMsg = await _httpClient.GetAsync("");
            if (responseMsg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListModel>>(await responseMsg.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<CategoryWithBlogsCountModel>> GetAllWithBlogsCount()
        {
            var msg = await _httpClient.GetAsync("GetWithBlogsCount");
            if (msg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryWithBlogsCountModel>>(await msg.Content.ReadAsStringAsync());
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

        public async Task AddAsync(CategoryAddModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", content);
        }
        public async Task UpdateAsync(CategoryUpdateModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PutAsync($"{model.Id}", content);
        }

        public async Task DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.DeleteAsync($"{id}");
        }
    }
}
