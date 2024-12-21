namespace CNTT36_WebXemPhim.DTO.Admin.Movie
{
    public class UpdateMovieDto
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string ReleaseDate { get; set; }

        public int Duration { get; set; }

        public string Language { get; set; } = null!;

        public string? ThumbnailUrl { get; set; }

        public string? ThumbnailUrl2 { get; set; }
    }
}
