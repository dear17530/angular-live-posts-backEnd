using System;
using System.Collections.Generic;

namespace Post.Models;

public partial class PostList
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }

    public string Author { get; set; }

    public DateTime DatetimeCreated { get; set; }

    public int NumberOfLikes { get; set; }

    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<UploadFile> UploadFiles { get; set; } = new List<UploadFile>();
}
