namespace CNTT36_WebXemPhim.DTO.Admin.Episode
{
    public class UpdateEpisodeDto
    {
        public int MovieId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string VideoUrl { get; set; }
        public string ReleaseDate { get; set; }
    }
}
