namespace CNTT36_WebXemPhim.DTO.Admin.Episode
{
    public class CreateEpisodeDto
    {
        public int CourseId { get; set; }
        public int LessonNumber { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string VideoUrl { get; set; }
        public string ReleaseDate { get; set; }
    }
}
