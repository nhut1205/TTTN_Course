using CNTT36_WebXemPhim.DTO.Customer.Course;
using CNTT36_WebXemPhim.DTO.Customer.Genre;
using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO.Customer
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [JsonIgnore]
        public DateOnly ReleaseDate { get; set; }
        public string Release_Date => ReleaseDate.ToString("dd/MM/yyyy");

        public int Duration { get; set; }

        public double? Rating { get; set; }

        public decimal Price { get; set; }

        public double? FormattedRating => Rating.HasValue ? Math.Round(Rating.Value, 1) : (double?)null;

        public string Language { get; set; } = null!;

        public string? ThumbnailUrl { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

        public string Created_At => CreatedAt.ToString("dd/MM/yyyy");
        public string Updated_At => UpdatedAt.ToString("dd/MM/yyyy");

        //public int? CountryId { get; set; }
        public string? ThumbnailUrl2 { get; set; }

        public List<LessonDto> Lessons { get; set; }
        public List<TeacherRoleCourseDto> TeacherRoleCourse { get; set; }
        public virtual List<CateWithCoursesDto> CateWithCourse { get; set; }
        public object CourseCate { get; internal set; }
    }
}
