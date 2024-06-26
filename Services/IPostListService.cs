using Microsoft.AspNetCore.Mvc;
using Post.Dtos.Post;
using Post.Models;
using Post.Parameters;

namespace Post.Services
{
    public interface IPostListService
    {
        public Task<IEnumerable<PostResDto>> QueryPost(PostResParamater value);
        public Task<PostResDto> QueryPostById(Guid id);
        public Task<PostList> CreatePost(PostReqDto value);
        public Task<PostList> UpdatePost(Guid id, PutReqDto value);
        public Task<int> DeletePost(Guid id);
        public Task<int> DeletePostByIds(List<Guid> ids);
    }
}
