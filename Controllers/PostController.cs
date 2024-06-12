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

        public PostController(PostContext postContext)
        {
            _postContext = postContext;
        }

        [HttpGet]
        public IEnumerable<PostResDto> Get([FromQuery]PostResParamater value)
        {
            var result = from a in _postContext.PostLists
                         select a;

            if(!string.IsNullOrEmpty(value.Title))
            {
                result = result.Where(x => x.Title.IndexOf(value.Title) > -1);
            }

            if (value.DatetimeCreated != null)
            {
                result = result.Where(x => x.DatetimeCreated.Date == value.DatetimeCreated);
            }

            return result.ToList().Select(a=>ItemToDto(a));
        }

        [HttpGet("{id}")]
        public PostResDto Get(Guid id)
        {
            var result = (from a in _postContext.PostLists
                         where a.Id == id
                         select ItemToDto(a)).SingleOrDefault();
            return result;
        }

        private static PostResDto ItemToDto(PostList postList)
        {
            return new PostResDto
            {
                Title = postList.Title,
                Description = postList.Description,
                ImagePath = postList.ImagePath,
                Author = postList.Author,
                DatetimeCreated = postList.DatetimeCreated,
                NumberOfLikes = postList.NumberOfLikes,
                Id = postList.Id,
            };
        }
    }
}
