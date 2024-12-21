using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Category
{
    public int CateId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CourseCate> CourseCates { get; set; } = new List<CourseCate>();
}
