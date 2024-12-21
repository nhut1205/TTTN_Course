using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Username { get; set; } = null!;

    public int CourseId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
