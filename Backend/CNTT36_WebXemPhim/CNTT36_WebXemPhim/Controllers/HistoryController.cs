using CNTT36_WebXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Thêm namespace này để sử dụng Include

namespace CNTT36_WebXemPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly SellCourseContext _context;

        public HistoryController(SellCourseContext context)
        {
            _context = context;
        }

        // POST: api/History/AddHistory
        [HttpPost("AddHistory")]
        public IActionResult AddHistory([FromBody] AddHistoryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data.");
            }

            // Kiểm tra sự tồn tại của người dùng
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Kiểm tra sự tồn tại của phim
            var movie = _context.Courses.FirstOrDefault(m => m.CourseId == request.MovieId);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            // Kiểm tra nếu lịch sử đã tồn tại
            var existingHistory = _context.Histories
                                          .FirstOrDefault(h => h.CourseId == request.MovieId && h.Username == request.Username);
            DateTime StartTime = DateTime.Now;
            // Tính toán EndTime tự động là 6 tháng sau StartTime
            DateTime endTime = StartTime.AddMonths(6);

            if (existingHistory != null)
            {
                // Nếu đã có lịch sử, cập nhật lại StartTime và EndTime
                existingHistory.StartTime = StartTime;
                existingHistory.EndTime = endTime;

                // Cập nhật vào cơ sở dữ liệu
                _context.Histories.Update(existingHistory);
            }
            else
            {
                // Nếu chưa có lịch sử, tạo mới
                var newHistory = new History
                {
                    CourseId = request.MovieId,
                    Username = request.Username,
                    StartTime = DateTime.Now,
                    EndTime = endTime
                };

                _context.Histories.Add(newHistory);
            }

            _context.SaveChanges();

            return Ok("History added or updated successfully.");
        }

        // GET: api/History/GetHistory/{username}
        [HttpGet("GetHistory/{username}")]
        public IActionResult GetHistory(string username)
        {
            // Kiểm tra sự tồn tại của người dùng
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Lấy toàn bộ lịch sử xem phim của người dùng và kèm theo tên bộ phim
            var historyList = _context.Histories
                                      .Where(h => h.Username == username)
                                      .Include(h => h.Course) // Kết hợp với bảng Movie để lấy tên bộ phim
                                      .OrderBy(h => h.StartTime) // Sắp xếp theo thời gian bắt đầu
                                      .ToList();

            if (historyList.Count == 0)
            {
                return NotFound("No history found for this user.");
            }

            // Chuyển đổi danh sách lịch sử thành dạng DTO để trả về tên bộ phim
            var result = historyList.Select(h => new
            {
                h.HistoryId,
                h.CourseId,
                h.Username,
                h.StartTime,
                h.EndTime,
                MovieTitle = h.Course.Title // Lấy tên bộ phim
            }).ToList();

            return Ok(result); // Trả về danh sách lịch sử cùng với tên bộ phim
        }
    }

    // DTO để nhận dữ liệu từ client
    public class AddHistoryRequest
    {
        public int MovieId { get; set; }
        public string Username { get; set; }
    }
}
