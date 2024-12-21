namespace CNTT36_WebXemPhim.DTO.Customer.Genre
{
    public class CourseCateWithCoursesDto
    {
        public int CourseCateID { get; set; }
        public int cateId { get; set; }

        // Liên kết với movie
        public List<CourseDto> Course2 { get; set; }
    }
}
