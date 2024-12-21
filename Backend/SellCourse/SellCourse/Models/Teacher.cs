using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public string? Bio { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public virtual ICollection<TeacherRoleCourse> TeacherRoleCourses { get; set; } = new List<TeacherRoleCourse>();
}
