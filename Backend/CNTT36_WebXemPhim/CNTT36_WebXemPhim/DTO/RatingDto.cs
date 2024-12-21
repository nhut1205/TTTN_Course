using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.DTO
{
    public class RatingDto
    {
        public int CourseId { get; set; }
        public string Username { get; set; }
        public double Rating { get; set; }
    }

}
