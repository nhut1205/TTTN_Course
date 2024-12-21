using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class RoleCourse
{
    public int RoleCourseId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<TeacherRoleCourse> TeacherRoleCourses { get; set; } = new List<TeacherRoleCourse>();
}
