using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Post.Dtos;

namespace Post.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 寫法2 var ignore = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            // [AllowAnonymousAttribute] 跟驗證的寫法一樣
            var ignore = (from a in context.ActionDescriptor.EndpointMetadata
                          where a.GetType() == typeof(AllowAnonymousAttribute)
                          select a).FirstOrDefault();

            if (ignore == null)
            {
                // TryGetValue 如果 Headers 有 Authorization 會回傳 bool, 且把值丟進 outValue
                bool tokenFlag = context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues outValue);

                if (tokenFlag)
                {
                    //if(outValue != "123") // 如果驗證不通過
                    //{
                    //    context.Result = new JsonResult(new ResultViewModel()
                    //    {
                    //        Data = "test1",
                    //        HttpCode = 401,
                    //        Message = "沒有登入"
                    //    });
                    //}
                }
                else // 如果 Headers 沒有傳入 Authorization
                {
                    context.Result = new JsonResult(new ResultViewModel()
                    {
                        Data = "test1",
                        HttpCode = 401,
                        Message = "沒有登入"
                    });
                }
            }
        }
    }
}
