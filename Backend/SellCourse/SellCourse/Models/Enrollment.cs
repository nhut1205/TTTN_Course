using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public string Username { get; set; } = null!;

    public int CourseId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public decimal? Progress { get; set; }

    public DateTime? CompletionDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
