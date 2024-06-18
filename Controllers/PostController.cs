using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Post.Dtos;
using Post.Models;
using Post.Parameters;
using Post.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Post.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostListService _postListService;

        public PostController(IPostListService postListService)
        {
            _postListService = postListService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PostResParamater value)
        {
            var result = _postListService.QueryPost(value);

            if (result == null || result.Count() == 0)
            {
                return NotFound("找不到資源");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIds([FromRoute] Guid id) 
        {
            var result = _postListService.QueryPostById(id);

            if (result == null)
            {
                return NotFound("找不到資源");
            }
    
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostReqDto value)
        {
            var result = _postListService.CreatePost(value);

            return CreatedAtAction(nameof(Get), new { Id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PutReqDto value)
        {
            var result = _postListService.UpdatePost(id, value);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _postListService.DeletePost(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteByIds([FromBody] List<Guid> ids)
        {
            var result = _postListService.DeletePostByIds(ids);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
