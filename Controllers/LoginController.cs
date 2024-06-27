using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Post.Dtos.Login;
using Post.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Post.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] // 允許匿名 不須身分驗證
    public class LoginController : ControllerBase
    {
        private readonly PostContext _postContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LoginController(PostContext postContext, IConfiguration configuration, IMapper mapper)
        {
            _postContext = postContext;
            _configuration = configuration;
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
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                // 產出 JWT Token
                // _configuration: 取得 appsettings.json 內的值
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"]));

                var jwt = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"], // 發行者
                    audience: _configuration["JWT:Audience"], // 給誰使用
                    claims: claims, // 資訊
                    expires: DateTime.Now.AddMinutes(30), // 期限
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256) // 產生方式
                );

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(token);
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
