using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Post.Dtos;
using Post.Models;
using Post.Parameters;

namespace Post.Services
{
    public class PostListService : IPostListService
    {
        private readonly PostContext _postContext;
        private readonly IMapper _mapper;
        public PostListService(PostContext postContext, IMapper mapper)
        {
            _postContext = postContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostResDto>> QueryPost(PostResParamater value)
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

            var resultList = await result.ToListAsync();

            return _mapper.Map<IEnumerable<PostResDto>>(resultList);
        }

        public async Task<PostResDto> QueryPostById(Guid id)
        {
            var result =await (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefaultAsync();

            return _mapper.Map<PostResDto>(result);
        }

        public async Task<PostList> CreatePost(PostReqDto value)
        {
            var map = _mapper.Map<PostList>(value);
            map.DatetimeCreated = DateTime.Now;

            _postContext.PostLists.Add(map);
            await _postContext.SaveChangesAsync();

            return map;
        }

        public async Task<PostList> UpdatePost(Guid id, PutReqDto value)
        {
            var update = await (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefaultAsync();

            if (update != null)
            {
                _mapper.Map(value, update);
                await _postContext.SaveChangesAsync();
            }

            return update;
        }

        public async Task<int> DeletePost(Guid id)
        {
            var delete = await(from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefaultAsync();

            if (delete != null)
            {
                _postContext.PostLists.Remove(delete);

            }

            return await _postContext.SaveChangesAsync(); // 會回傳修改的數量
        }

        public async Task<int> DeletePostByIds(List<Guid> ids)
        {
            var delete = from a in _postContext.PostLists
                         where ids.Contains(a.Id)
                         select a;

            if (delete != null)
            {
                _postContext.PostLists.RemoveRange(delete);
            }

            return await _postContext.SaveChangesAsync();
        }
    }
}
