
using CNTT36_WebXemPhim.DTO.Customer;
using CNTT36_WebXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CNTT36_WebXemPhim.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly SellCourseContext _context;

        public MoviesController(SellCourseContext context)
        {
            _context = context;
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchMoviesByTitle([FromQuery] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title parameter is required.");
            }

            try
            {
                var movies = await _context.Courses
                    .Where(m => m.Title.ToLower().Contains(title.ToLower())) // Chuyển đổi về chữ thường
                    .Select(m => new CourseDto
                    {
                        CourseId = m.CourseId,
                        Title = m.Title,
                        Description = m.Description,
                        ReleaseDate = m.ReleaseDate,
                        Duration = m.Duration,
                        Price = m.Price,
                        Rating = m.Rating,
                        Language = m.Language,
                        ThumbnailUrl = m.ThumbnailUrl,
                        ThumbnailUrl2 = m.ThumbnailUrl2,
                        // Các trường cần thiết khác
                    })
                    .ToListAsync();

                if (!movies.Any())
                {
                    return NotFound("No movies found with the specified title.");
                }

                return Ok(movies); // Trả về dữ liệu DTO thay vì dữ liệu thô
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
