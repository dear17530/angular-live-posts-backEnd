using AutoMapper;
using Post.Dtos;
using Post.Models;

namespace Post.Profiles
{
    public class UploadFileProfile : Profile
    {
        public UploadFileProfile()
        {
            CreateMap<UploadFileDto, UploadFile>();
        }
    }
}
