using AutoMapper;
using Post.Dtos;
using Post.Models;

namespace Post.Profiles
{
    public class PostListProfile : Profile
    {
        public PostListProfile()
        {
            CreateMap<PostList, PostResDto>();
        }
    }
}
