using Post.Dtos;
using Post.Models;
using System.ComponentModel.DataAnnotations;

namespace Post.ValidationAttributes
{
    public class PostTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 取得 service
            PostContext _postContext = (PostContext)validationContext.GetService(typeof(PostContext));

            var title = (string)value;

            var findTitle = from a in _postContext.PostLists
                            where a.Title == title
                            select a;

            // 確認傳入的 Dto 是否是 PutReqDto
            var dto = validationContext.ObjectInstance;

            if(dto.GetType() == typeof(PutReqDto))
            {
                var dtoUpate = (PutReqDto)dto;
                findTitle = findTitle.Where(a => a.Id != dtoUpate.Id);
            }

            if(findTitle.FirstOrDefault() != null)
            {
                return new ValidationResult("已存在相同的代辦事項");
            }

            return ValidationResult.Success;
        }
    }
}
