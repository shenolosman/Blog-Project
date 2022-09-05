using BlogProject.Business.Containers.MicrosoftIoC;
using BlogProject.Business.Interfaces;
using BlogProject.Business.StringInfos;
using BlogProject.WebApi;
using BlogProject.WebApi.CustomFilters;
using BlogProject.WebApi.Mapping.AutoMapperProfile;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDependencies();

builder.Services.AddAutoMapper(typeof(MapProfile));  //typeof(Program).Assembly
builder.Services.Configure<JwtInfo>(builder.Configuration.GetSection("JwtInfo"));
var jwtInfo = builder.Configuration.GetSection("JwtInfo").Get<JwtInfo>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Blog Api",
        Description = "Blog Api Document",
        Contact = new OpenApiContact()
        {
            Email = "admin@mail.com",
            Name = "admin malcom"
        }
    });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "UseAuthorization",
        Type = SecuritySchemeType.Http,
        Description = "Bearer {token}"
    });
});
builder.Services.AddScoped(typeof(ValidId<>));

builder.Services.AddMemoryCache();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtInfo.Issuer,
        ValidAudience = jwtInfo.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecurityKey)),
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseExceptionHandler("/api/Error");
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope())
{
    var appUserService = scope.ServiceProvider.GetRequiredService<IAppUserService>();

    JwtIdentityInitializer.Seed(appUserService).Wait();
}
app.Run();
