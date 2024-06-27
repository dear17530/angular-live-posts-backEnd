using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Post.Dtos.Login;
using Post.Models;
using System.Security.Claims;

namespace Post.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] // 允許匿名 不須身分驗證
    public class LoginController : ControllerBase
    {
        private readonly PostContext _postContext;
        private readonly IMapper _mapper;

        public LoginController(PostContext postContext, IMapper mapper)
        {
            _postContext = postContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login(ReqLoginDto value)
        {
            var user = await(from a in _postContext.Employees
                             where a.Account == value.Account
                             && a.Password == value.Password
                             select a).SingleOrDefaultAsync();
            if (user == null)
            {
                return Ok("帳號密碼錯誤");
            } else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("FullName", user.Name),
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                // 也有其他屬性可以設定
                //var authProperties = new AuthenticationProperties
                //{
                //    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(2)
                //};

                // AddSeconds 只有這隻 API 受影響
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok("登入成功");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("登出成功");
        }

        [HttpGet("NoLogin")]
        public string noLogin()
        {
            return "未登入";
        }

        [HttpGet("NoAccess")]
        public string noAccess()
        {
            return "沒有權限";
        }
    }
}
