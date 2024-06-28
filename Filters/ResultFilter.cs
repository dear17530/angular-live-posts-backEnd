using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Post.Dtos;

namespace Post.Filters
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var contextResult = context.Result as ObjectResult; // 轉型成 ObjectResult

            if (context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new ResultViewModel()
                {
                    Data = contextResult.Value, // 不加上 .value 的話, 轉出來會多一層 .value
                });

            } else
            {
                context.Result = new JsonResult(new ResultViewModel()
                {
                    Error = contextResult.Value,
                });
            }
        }
    }
}
