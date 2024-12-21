using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class History
{
    public int HistoryId { get; set; }

    public int CourseId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
