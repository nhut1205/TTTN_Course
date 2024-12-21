using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class DetailUser
{
    public int DetailId { get; set; }

    public string Username { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? AvatarUrl { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? FullName { get; set; }

    public virtual User UsernameNavigation { get; set; } = null!;
}
