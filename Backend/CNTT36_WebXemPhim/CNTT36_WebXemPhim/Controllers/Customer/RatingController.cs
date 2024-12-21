using CNTT36_WebXemPhim.DTO;
using CNTT36_WebXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly SellCourseContext _context;

        public RatingController(SellCourseContext context)
        {
            _context = context;
        }

        // POST: api/Rating
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingDto ratingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra xem phim có tồn tại không
            var movie = await _context.Courses.FindAsync(ratingDto.CourseId);
            if (movie == null)
            {
                return NotFound("Courses not found");
            }

            // Kiểm tra xem người dùng có tồn tại không
            var user = await _context.Users.FindAsync(ratingDto.Username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Kiểm tra xem người dùng đã đánh giá phim này chưa
            var existingRating = await _context.RatingFilms
                .FirstOrDefaultAsync(r => r.CourseId == ratingDto.CourseId && r.Username == ratingDto.Username);

            if (existingRating != null)
            {
                // Cập nhật đánh giá hiện tại
                existingRating.Rating = ratingDto.Rating;
                _context.RatingFilms.Update(existingRating);
            }
            else
            {
                // Tạo đánh giá mới
                var ratingFilm = new RatingFilm
                {
                    CourseId = ratingDto.CourseId,
                    Username = ratingDto.Username,
                    Rating = ratingDto.Rating
                };
                _context.RatingFilms.Add(ratingFilm);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Rating added/updated successfully" });
        }

        // GET: api/Rating/CheckRating
        [HttpGet("CheckRating")]
        public async Task<IActionResult> CheckRating(int courseId, string username)
        {
            var rating = await _context.RatingFilms
                .FirstOrDefaultAsync(r => r.CourseId == courseId && r.Username == username);

            if (rating == null)
            {
                return Ok(new { rating = 0 }); // Không có đánh giá, trả về 0
            }

            return Ok(new { rating = rating.Rating }); // Trả về giá trị đánh giá
        }

        //Danh sách film đã đánh giá  
        [HttpGet("UserRatings/{username}")]
        public async Task<IActionResult> GetUserRatings(string username)
        {
            var userRatings = await _context.RatingFilms
                .Include(r => r.Course)
                .Where(r => r.Username == username)
                .Select(r => new
                {
                    CourseId = r.CourseId,
                    MovieTitle = r.Course.Title,
                    Rating = r.Rating,
                })
                .ToListAsync();

            if (!userRatings.Any())
            {
                return NotFound("No ratings found for this user.");
            }

            return Ok(userRatings);
        }

        // GET: api/Rating/MovieRatings/{courseId}
        [HttpGet("MovieRatings/{courseId}")]
        public async Task<IActionResult> GetMovieRatings(int courseId)
        {
            var movieRatings = await _context.RatingFilms
                .Where(r => r.CourseId == courseId)
                .Select(r => new
                {
                    Username = r.Username,
                    Rating = r.Rating,
                })
                .ToListAsync();

            if (!movieRatings.Any())
            {
                return NotFound("No ratings found for this movie.");
            }

            return Ok(movieRatings);
        }

    }

}
