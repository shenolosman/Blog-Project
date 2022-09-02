using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Extensions;
using BlogProject.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BlogProject.Web.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/Blogs/"); // from api applicationUrl in launchSettings
        }
        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseMsg = await _httpClient.GetAsync("");
            if (responseMsg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMsg.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var responseMsg = await _httpClient.GetAsync($"{id}");
            if (responseMsg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogListModel>(await responseMsg.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id)
        {
            var msg = await _httpClient.GetAsync($"GetAllByCategoryId/{id}");
            if (msg.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await msg.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task AddAsync(BlogAddModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (model.Image != null)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);
                ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);

                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteArrayContent, nameof(BlogAddModel.Image), model.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;
            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));

            formData.Add(new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));
            formData.Add(new StringContent(model.Description), nameof(BlogAddModel.Description));
            formData.Add(new StringContent(model.Title), nameof(BlogAddModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", formData);
        }
    }
}
