using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Post.Models;
using Post.Services;

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

// Cookie ����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    // ���n�J�ɷ|�۰ʾɨ�o�Ӻ��}
    option.LoginPath = new PathString("/api/Login/NoLogin");
    // �S�v���ɷ|�۰ʾɨ�o�Ӻ��}
    option.AccessDeniedPath = new PathString("/api/Login/NoAccess");
    // ������ API �n�J�ɶ��]�w
    // option.ExpireTimeSpan = TimeSpan.FromSeconds(2);
});

// ����[�W�v��
// 1. �L�����Ҫ� api �i�H�[�W [AllowAnonymous]
// 2. �� swagger ���շ|�� cors ���D
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//���ǭn�@��
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
