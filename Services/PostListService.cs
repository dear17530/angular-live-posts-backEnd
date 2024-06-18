using AutoMapper;
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

        public IEnumerable<PostResDto> QueryPost(PostResParamater value)
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

            return _mapper.Map<IEnumerable<PostResDto>>(result);
        }

        public PostResDto QueryPostById(Guid id)
        {
            var result = (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefault();

            return _mapper.Map<PostResDto>(result);
        }

        public PostList CreatePost(PostReqDto value)
        {
            var map = _mapper.Map<PostList>(value);
            map.DatetimeCreated = DateTime.Now;

            _postContext.PostLists.Add(map);
            _postContext.SaveChanges();

            return map;
        }

        public PostList UpdatePost(Guid id, PutReqDto value)
        {
            var update = (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                _mapper.Map(value, update);
                _postContext.SaveChanges();
            }

            return update;
        }

        public int DeletePost(Guid id)
        {
            var delete = (from a in _postContext.PostLists
                          where a.Id == id
                          select a).SingleOrDefault();

            if (delete != null)
            {
                _postContext.PostLists.Remove(delete);

            }

            return _postContext.SaveChanges(); // 會回傳修改的數量
        }

        public int DeletePostByIds(List<Guid> ids)
        {
            var delete = from a in _postContext.PostLists
                         where ids.Contains(a.Id)
                         select a;

            if (delete != null)
            {
                _postContext.PostLists.RemoveRange(delete);
            }

            return _postContext.SaveChanges();
        }
    }
}
