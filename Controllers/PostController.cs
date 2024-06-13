using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Post.Dtos;
using Post.Models;
using Post.Parameters;
using System;

namespace Post.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostContext _postContext;
        private readonly IMapper _mapper;

        public PostController(PostContext postContext, IMapper mapper)
        {
            _postContext = postContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PostResParamater value) // 使用 IActionResult 示範
        {
            var result = from a in _postContext.PostLists
                         select a;

            if (!string.IsNullOrEmpty(value.Title))
            {
                result = result.Where(x => x.Title.IndexOf(value.Title) > -1);
            }

            if (value.DatetimeCreated != null)
            {
                result = result.Where(x => x.DatetimeCreated.Date == value.DatetimeCreated);
            }

            if (result == null || result.Count() == 0)
            {
                return NotFound("找不到資源");
            }

            return Ok(_mapper.Map<IEnumerable<PostResDto>>(result));
        }

        [HttpGet("{id}")]
        public ActionResult<PostResDto> Get(Guid id)  // 使用 ActionResult 示範, return 可以省略 Ok
        {
            var result = (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound("找不到資源");
            }

            return _mapper.Map<PostResDto>(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostReqDto value)
        {
            PostList insert = new PostList
            {
                DatetimeCreated = DateTime.Now,
            };

            _postContext.PostLists.Add(insert).CurrentValues.SetValues(value);
            _postContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { Id = insert.Id }, insert);
        }
    }
}
