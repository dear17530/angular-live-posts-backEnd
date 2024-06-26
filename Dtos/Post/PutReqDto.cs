using Post.ValidationAttributes;

namespace Post.Dtos.Post
{
    [StartTimeEndTime]
    public class PutReqDto
    {
        [PostTitle] // 也可寫成 [PostTitle]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
