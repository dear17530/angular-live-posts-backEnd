using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Post.Models;
using System.Security.Claims;

namespace Post.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly IWebHostEnvironment _env;
        public ActionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string rootRoot = _env.ContentRootPath + @"\Log\";

            if (!Directory.Exists(rootRoot))
            {
                Directory.CreateDirectory(rootRoot);
            }

            var user = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var path = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;

            string text = "結束: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " path: " + path + " method: " + method + " user: " + user;
            File.AppendAllText(rootRoot + DateTime.Now.ToString("yyyyMMdd") + ".txt", text);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string rootRoot = _env.ContentRootPath + @"\Log\";

            if(!Directory.Exists(rootRoot))
            {
                Directory.CreateDirectory(rootRoot);
            }

            var user = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var path = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;

            string text = "開始: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " path: " + path + " method: " + method + " user: " + user;
            // 通常不會放專案內 或是可以開 table 存 log
            File.AppendAllText(rootRoot + DateTime.Now.ToString("yyyyMMdd") + ".txt", text);
        }
    }
}
