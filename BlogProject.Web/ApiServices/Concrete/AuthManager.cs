using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace BlogProject.Web.ApiServices.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthManager(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/Auth/");
        }
        public async Task<bool> SignIn(AppUserLoginModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);

            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var msg = await _httpClient.PostAsync("SignIn", stringContent);
            if (msg.IsSuccessStatusCode)
            {
                var accessToken = JsonConvert.DeserializeObject<AccessToken>(await msg.Content.ReadAsStringAsync());
                _contextAccessor.HttpContext.Session.SetString("token", accessToken.Token);
                return true;
            }
            return false;
        }
    }
}
