using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO.Admin.Episode
{
    public class EpisodeAdminDto
    {
        public int LessonId { get; set; }
        //public string MovieTitle { get; set; }
        public int LessonNumber { get; set; }
        public string? Title { get; set; }
        public int Duration { get; set; }
        public string VideoUrl { get; set; } = null!;

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        public string Created_At => CreatedAt.ToString("dd/MM/yyyy");

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        public string Updated_At => UpdatedAt.ToString("dd/MM/yyyy");

        [JsonIgnore]
        public DateOnly ReleaseDate { get; set; }
        public string Release_Date => ReleaseDate.ToString("dd/MM/yyyy");
    }
}
