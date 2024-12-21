using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int CourseId { get; set; }

    public int LessonNumber { get; set; }

    public string? Title { get; set; }

    public int Duration { get; set; }

    public string VideoUrl { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public virtual Course Course { get; set; } = null!;
}
