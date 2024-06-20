using Post.Models;
using Post.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Post.Dtos
{
    public class UploadFileDto
    {
        public string Name { get; set; }

        public string Src { get; set; }

        public Guid Id { get; set; }

        public Guid PostId { get; set; }

    }
}
