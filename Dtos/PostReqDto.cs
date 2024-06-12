namespace Post.Dtos
{
    public class PostReqDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string? Author { get; set; }
    }
}
