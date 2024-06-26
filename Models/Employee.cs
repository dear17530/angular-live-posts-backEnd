using System;
using System.Collections.Generic;

namespace Post.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Account { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
}
