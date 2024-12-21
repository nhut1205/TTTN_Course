using System;
using System.Collections.Generic;

namespace CNTT36_WebXemPhim.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public int Duration { get; set; }

    public double? Rating { get; set; }

    public string Language { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ThumbnailUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? ThumbnailUrl2 { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CourseCate> CourseCates { get; set; } = new List<CourseCate>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<RatingFilm> RatingFilms { get; set; } = new List<RatingFilm>();

    public virtual ICollection<TeacherRoleCourse> TeacherRoleCourses { get; set; } = new List<TeacherRoleCourse>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
