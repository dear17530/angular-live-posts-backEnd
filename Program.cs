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

// Cookie ����
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
//{
//    // ���n�J�ɷ|�۰ʾɨ�o�Ӻ��}
//    option.LoginPath = new PathString("/api/Login/NoLogin");
//    // �S�v���ɷ|�۰ʾɨ�o�Ӻ��}
//    option.AccessDeniedPath = new PathString("/api/Login/NoAccess");
//    // ������ API �n�J�ɶ��]�w
//    // option.ExpireTimeSpan = TimeSpan.FromSeconds(2);
//});

// JwtBearerDefaults �ݦw�ˮM��
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // �O�_���� Issuer
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // �]�w Issuer ����
        ValidateAudience = true, // �O�_���� Audience
        ValidAudience = builder.Configuration["Jwt:Audience"], // �]�w Audience ����
        ValidateLifetime = true, // �O�_���Ҩ���ɶ�(�w�]�� true)
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"])) // �p�_�]�w
    };
}
);

// ����[�W�v��
// 1. �L�����Ҫ� api �i�H�[�W [AllowAnonymous]
// 2. �� swagger ���շ|�� cors ���D
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

// �b�ۭq������� �ݭn�t�~�`�J
builder.Services.AddHttpContextAccessor();

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
