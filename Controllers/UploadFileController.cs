using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Post.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Post.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UploadFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly PostContext _postContext;
        private readonly IMapper _mapper;


        public UploadFileController(IWebHostEnvironment env, PostContext postContext, IMapper mapper)
        {
            _env = env;
            _postContext = postContext;
            _mapper = mapper;
        }

        // POST api/<UploadFileController>
        [HttpPost("{id}")]
        public void Post(IFormFileCollection files, Guid id)
        {
            string rootRoot = _env.ContentRootPath + @"\wwwroot\UploadFiles\" + id + "\\";

            // 確認資料夾是否存在
            if (!Directory.Exists(rootRoot))
            {
                // 不存在則創建
                Directory.CreateDirectory(rootRoot);
            }

            foreach (var file in files)
            {
                string fileName = file.FileName;

                using (var stream = System.IO.File.Create(rootRoot + fileName))
                {
                    file.CopyTo(stream);

                    var insert = new UploadFile
                    {
                        Name = fileName,
                        Src = "/UploadFiles" + id + "/" + fileName,
                        PostId = id,
                    };

                    _postContext.UploadFiles.Add(insert);
                }
            }

            _postContext.SaveChanges(); 
        }
    }
}
