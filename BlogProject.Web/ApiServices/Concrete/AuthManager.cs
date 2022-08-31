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
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("SignIn", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var accessToken = JsonConvert.DeserializeObject<AccessToken>(await responseMessage.Content.ReadAsStringAsync());

                _contextAccessor.HttpContext.Session.SetString("token", accessToken.Token);

                return true;
            }
            return false;
        }
    }
}
