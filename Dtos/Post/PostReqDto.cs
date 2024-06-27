using Post.Models;
using Post.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Post.Dtos.Post
{
    public class PostReqDto
    {
        [PostTitle]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
