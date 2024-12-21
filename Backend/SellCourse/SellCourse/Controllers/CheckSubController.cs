using CNTT36_WebXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CheckSubController : ControllerBase
    {
        private readonly SellCourseContext _context;

        public CheckSubController(SellCourseContext context)
        {
            _context = context;
        }



		// Lấy danh sách tập phim và kiểm tra quyền truy cập
		[HttpGet("episodes/{movieId}")]
		public IActionResult GetEpisodes(int movieId, string username)
		{
			// Kiểm tra xem username có được cung cấp không
			if (string.IsNullOrWhiteSpace(username))
			{
				return BadRequest("Username is required.");
			}

			// Lấy thông tin người dùng dựa trên username
			var user = _context.Users
				.Where(u => u.Username == username)
				.Select(u => new
				{
					u.Username,
					u.MembershipStatus
				})
				.FirstOrDefault();

			// Nếu không tìm thấy user, trả về lỗi
			if (user == null)
			{
				return NotFound("User not found.");
			}

			// Lấy thông tin phim từ movieId và danh sách tập phim
			var movie = _context.Courses
				.Where(m => m.CourseId == movieId)
				.Select(m => new
				{
					m.CourseId,
					Episodes = m.Lessons.OrderBy(e => e.LessonNumber).Select(e => new
					{
						e.LessonNumber,
						e.VideoUrl
					}).ToList()
				})
				.FirstOrDefault();

			// Nếu không tìm thấy phim, trả về lỗi
			if (movie == null)
			{
				return NotFound("Movie not found.");
			}

			// Tạo danh sách tập phim dựa trên MembershipStatus
			var episodes = user.MembershipStatus == "Premium"
				? movie.Episodes.Select(episode => new
				{
					EpisodeNumber = episode.LessonNumber,
					VideoUrl = episode.VideoUrl,
					IsAccessible = true // Premium: truy cập được tất cả tập
				}).ToList()
				: movie.Episodes
					.Take(3) // Free: chỉ hiển thị 3 tập đầu tiên
					.Select(episode => new
					{
						EpisodeNumber = episode.LessonNumber,
						VideoUrl = episode.VideoUrl,
						IsAccessible = true
					}).ToList();

			// Trả về danh sách tập phim
			return Ok(new { Episodes = episodes });
		}

	}
}