﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Post.Dtos;
using Post.Models;
using Post.Parameters;
using System;
using System.Collections.Generic;
using System.Text.Json;

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

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PutReqDto value)
        {
            var update = (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                _postContext.PostLists.Update(update).CurrentValues.SetValues(value);
                _postContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            //var child = from a in _postContext.childLists
            //            where a.ListId == id
            //            select a;

            //_postContext.childLists.RemoveRange(child);
            //_postContext.SaveChanges();

            var delete = (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefault();

            if (delete == null)
            {
                return NotFound();
            }

            _postContext.PostLists.Remove(delete);
            _postContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public void Delete([FromBody] List<Guid> ids)
        {
            var delete = from a in _postContext.PostLists
                         where ids.Contains(a.Id)
                         select a;

            if (delete != null)
            {
                _postContext.PostLists.RemoveRange(delete);
                _postContext.SaveChanges();
            }
        }
    }
}
