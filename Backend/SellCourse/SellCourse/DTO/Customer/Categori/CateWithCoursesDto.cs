namespace CNTT36_WebXemPhim.DTO.Customer.Genre
{
    public class CateWithCoursesDto
    {
        public int cateId { get; set; }
        public string Name { get; set; } = null!;
        public List<CourseCateWithCoursesDto> CourseCate2 { get; set; }
    }
}
