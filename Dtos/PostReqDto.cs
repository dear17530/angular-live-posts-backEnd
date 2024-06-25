using Post.Models;
using Post.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Post.Dtos
{
    public class PostReqDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
