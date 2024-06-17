using Post.Models;
using Post.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Post.Dtos
{
    public class PostReqDto : IValidatableObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 取得 service
            PostContext _postContext = (PostContext)validationContext.GetService(typeof(PostContext));

            var findTitle = from a in _postContext.PostLists
                            where a.Title == Title
                            select a;

            // 確認傳入的 Dto 是否是 PutReqDto
            var dto = validationContext.ObjectInstance;

            if (dto.GetType() == typeof(PutReqDto))
            {
                var dtoUpate = (PutReqDto)dto;
                findTitle = findTitle.Where(a => a.Id != dtoUpate.Id);
            }

            if (findTitle.FirstOrDefault() != null)
            {
                yield return new ValidationResult("已存在相同的代辦事項", ["title"]);
            }


            if (StartTime >=EndTime)
            {
                yield return new ValidationResult("開始時間不可以大於結束時間", ["time"]);
            }

            yield return ValidationResult.Success;
        }
    }
}
