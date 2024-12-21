using CNTT36_WebXemPhim.DTO.Customer.Course;
using CNTT36_WebXemPhim.Models;

namespace CNTT36_WebXemPhim.DTO.Customer.Movie
{
    public class CourseCateDto
    {
        public int courseCateId { get; set; }

        public int courseId { get; set; }
        public int cateId { get; set; }

        public List<CateDto> categories { get; set; }
    }
}
