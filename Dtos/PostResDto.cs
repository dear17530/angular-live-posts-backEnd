using Post.ValidationAttributes;

namespace Post.Dtos
{
    [StartTimeEndTimeAttribute]
    public class PostResDto
    {
        [PostTitleAttribute]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public DateTime DatetimeCreated { get; set; }
        public int NumberOfLikes { get; set; }
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
