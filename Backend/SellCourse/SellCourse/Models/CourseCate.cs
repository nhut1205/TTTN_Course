using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class CourseCate
{
    public int CourseCateId { get; set; }

    public int CourseId { get; set; }

    public int CateId { get; set; }

    public virtual Category Cate { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
