using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO.Admin.Movie
{
    public class CreateMovieDto
    {

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        //[JsonIgnore]
        public string ReleaseDate { get; set; }

        public int Duration { get; set; }

        //public double? Rating { get; set; }

        public string Language { get; set; } = null!;

        public string? ThumbnailUrl { get; set; }

        //public int? CountryId { get; set; }
        public string? ThumbnailUrl2 { get; set; }
    }
}
