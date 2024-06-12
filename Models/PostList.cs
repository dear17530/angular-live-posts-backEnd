using System;
using System.Collections.Generic;

namespace Post.Models;

public partial class PostList
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public string Author { get; set; }

    public DateTime DatetimeCreated { get; set; }

    public int NumberOfLikes { get; set; }

    public Guid Id { get; set; }
}
