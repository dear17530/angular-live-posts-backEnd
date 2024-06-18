using Microsoft.AspNetCore.Mvc;
using Post.Dtos;
using Post.Models;
using Post.Parameters;

namespace Post.Services
{
    public interface IPostListService
    {
        public IEnumerable<PostResDto> QueryPost(PostResParamater value);
        public PostResDto QueryPostById(Guid id);
        public PostList CreatePost(PostReqDto value);
        public PostList UpdatePost(Guid id, PutReqDto value);
        public int DeletePost(Guid id);
        public int DeletePostByIds(List<Guid> ids);
    }
}
