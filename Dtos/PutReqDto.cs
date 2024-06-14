using Post.ValidationAttributes;

namespace Post.Dtos
{
    public class PutReqDto
    {
        [PostTitleAttribute] // 也可寫成 [PostTitle]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public Guid Id { get; set; }
    }
}
