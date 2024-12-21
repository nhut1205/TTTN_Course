

namespace CNTT36_WebXemPhim.Models;

public partial class TeacherRoleCourse
{
    public int TeacherRoleCourseId { get; set; }

    public int? TeacherId { get; set; }

    public int? RoleCourseId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual RoleCourse? RoleCourse { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
