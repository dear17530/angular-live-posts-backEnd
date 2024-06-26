using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Post.Dtos.Post;
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
    //[Authorize] 整個 controller 加上權限
    public class PostController : ControllerBase
    {
        private readonly IPostListService _postListService;

        public PostController(IPostListService postListService)
        {
            _postListService = postListService;
        }

        [HttpGet]
        //[Authorize] 單支 api 加上權限
        public async Task<IActionResult> Get([FromQuery] PostResParamater value)
        {
            var result = await _postListService.QueryPost(value);

            if (result == null || result.Count() == 0)
            {
                return NotFound("找不到資源");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIds([FromRoute] Guid id) 
        {
            var result = await _postListService.QueryPostById(id);

            if (result == null)
            {
                return NotFound("找不到資源");
            }
    
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostReqDto value)
        {
            var result = await _postListService.CreatePost(value);

            return CreatedAtAction(nameof(Get), new { Id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PutReqDto value)
        {
            var result = await _postListService.UpdatePost(id, value);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _postListService.DeletePost(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIds([FromBody] List<Guid> ids)
        {
            var result = await _postListService.DeletePostByIds(ids);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
