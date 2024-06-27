using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Post.Models;
using Post.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PostDatabase")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// IOC
builder.Services.AddScoped<IPostListService, PostListService>();

// Cookie 驗證
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
//{
//    // 未登入時會自動導到這個網址
//    option.LoginPath = new PathString("/api/Login/NoLogin");
//    // 沒權限時會自動導到這個網址
//    option.AccessDeniedPath = new PathString("/api/Login/NoAccess");
//    // 全部的 API 登入時間設定
//    // option.ExpireTimeSpan = TimeSpan.FromSeconds(2);
//});

// JwtBearerDefaults 需安裝套件
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // 是否驗證 Issuer
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // 設定 Issuer 的值
        ValidateAudience = true, // 是否驗證 Audience
        ValidAudience = builder.Configuration["Jwt:Audience"], // 設定 Audience 的值
        ValidateLifetime = true, // 是否驗證到期時間(預設為 true)
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"])) // 私鑰設定
    };
}
);

// 全域加上權限
// 1. 無須驗證的 api 可以加上 [AllowAnonymous]
// 2. 用 swagger 測試會有 cors 問題
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

// 在自訂元件取值 需要另外注入
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//順序要一樣
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
