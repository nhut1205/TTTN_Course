using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public string Username { get; set; } = null!;

    public int CourseId { get; set; }

    public DateTime? AddedDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
