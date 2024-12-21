using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string Decriptions { get; set; } = null!;

    public string ThumnailUrl { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
