using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Post.Dtos;

namespace Post.Filters
{
    public class FileLimitFilter : Attribute, IResourceFilter
    {
        public long Size = 5;
        // 傳出
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        // 傳入
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var files = context.HttpContext.Request.Form.Files;
            
            foreach (var file in files)
            {
                if(file.Length > (1024 * 1024 * Size))
                {
                    context.Result = new JsonResult(new ResultViewModel()
                    {
                        Data = false,
                        HttpCode = 400,
                        Message = "檔案太大瞜"
                    });
                }

                if (Path.GetExtension(file.FileName) != ".mp4")
                {
                    context.Result = new JsonResult(new ResultViewModel()
                    {
                        Data = false,
                        HttpCode = 400,
                        Message = "只允許上傳mp4"
                    });
                }
            }
        }
    }
}
