﻿using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Extensions;
using BlogProject.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
            _httpClient.BaseAddress = new Uri("http://localhost:52395/api/Blogs/"); // from api applicationUrl in launchSettings
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
                var bytes = await File.ReadAllBytesAsync(model.Image.FileName, CancellationToken.None);
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(BlogAddModel.Image), model.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;

            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));

            formData.Add(new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));

            formData.Add(new StringContent(model.Description), nameof(BlogAddModel.Description));

            formData.Add(new StringContent(model.Title), nameof(BlogAddModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", formData);
        }
        public async Task UpdateAsync(BlogUpdateModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (model.Image != null)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(BlogAddModel.Image), model.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;
            formData.Add(new StringContent(model.Id.ToString()), nameof(BlogUpdateModel.Id));

            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));

            formData.Add(new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));

            formData.Add(new StringContent(model.Description), nameof(BlogAddModel.Description));

            formData.Add(new StringContent(model.Title), nameof(BlogAddModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PutAsync($"{model.Id}", formData);
        }

        public async Task DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                _httpContextAccessor.HttpContext.Session.GetString("token"));
            var msg = await _httpClient.DeleteAsync($"{id}");
        }

        public async Task<List<CommentListModel>> GetCommentsAsync(int blogId, int? parentCommentId)
        {
            var responseMessage = await _httpClient.GetAsync($"{blogId}/GetComments?parentCommentId={parentCommentId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CommentListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task AddToComment(CommentAddModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("AddComment", content);
        }

        public async Task<List<CategoryListModel>> GetCategoriesAsync(int blogId)
        {
            var responseMessage = await _httpClient.GetAsync($"{blogId}/GetCategories");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListModel>>(await responseMessage.Content
                    .ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetLastFiveAsync()
        {
            var responseMessage = await _httpClient.GetAsync("GetLastFive");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content
                    .ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> SearchAsync(string s)
        {
            var responseMessage = await _httpClient.GetAsync($"Search?s={s}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content
                    .ReadAsStringAsync());
            }
            return null;
        }

        public async Task AddToCategoryAsync(CategoryBlogModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("AddToCategory", content);
        }

        public async Task RemoveFromCategoryAsync(CategoryBlogModel model)
        {
            await _httpClient.DeleteAsync($"RemoveFromCategory/{nameof(CategoryBlogModel.CategoryId)}={model.CategoryId}&{nameof(CategoryBlogModel.BlogId)}={model.BlogId}");
        }
    }
}
