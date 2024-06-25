using System;
using System.Collections.Generic;

namespace Post.Models;

public partial class UploadFile
{
    public string Name { get; set; }

    public string Src { get; set; }

    public Guid Id { get; set; }

    public Guid PostId { get; set; }
}
