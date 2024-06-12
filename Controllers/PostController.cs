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

            return _mapper.Map<IEnumerable<PostResDto>>(result);
        }

        [HttpGet("{id}")]
        public PostResDto Get(Guid id)
        {
            var result = (from a in _postContext.PostLists
                         where a.Id == id
                         select a).SingleOrDefault();

            return _mapper.Map<PostResDto>(result);
        }
    }
}
