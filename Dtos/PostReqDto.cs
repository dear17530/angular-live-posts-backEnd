using Post.ValidationAttributes;

namespace Post.Dtos
{
    [StartTimeEndTimeAttribute]
    public class PostReqDto
    {
        [PostTitleAttribute] // 也可寫成 [PostTitle]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
