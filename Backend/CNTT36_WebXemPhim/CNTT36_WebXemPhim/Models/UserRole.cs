using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string Username { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
