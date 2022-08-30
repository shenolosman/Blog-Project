using BlogProject.Web.ApiServices.Concrete;
using BlogProject.Web.ApiServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IBlogApiService, BlogApiManager>();
builder.Services.AddHttpClient<ICategoryService, CategoryManager>();
builder.Services.AddHttpClient<IImageApiService, ImageApiManager>();
builder.Services.AddHttpClient<IAuthService, AuthManager>();

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();
app.UseSession();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
