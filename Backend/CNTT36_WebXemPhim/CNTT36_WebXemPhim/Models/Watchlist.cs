﻿namespace CNTT36_WebXemPhim.Models;

public partial class Watchlist
{
    public int WatchlistId { get; set; }

    public string Username { get; set; } = null!;

    public int CourseId { get; set; }

    public DateTime? AddedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
