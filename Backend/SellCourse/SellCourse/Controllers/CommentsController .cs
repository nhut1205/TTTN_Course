using CNTT36_WebXemPhim.DTO;
using CNTT36_WebXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly SellCourseContext _context;

        public CommentsController(SellCourseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostComment([FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest("Invalid comment data.");
            }

            // Tạo đối tượng Comment từ CommentDto
            var comment = new Comment
            {
                Username = commentDto.Username,
                CourseId = commentDto.MovieId,
                Content = commentDto.Content,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, comment);
        }

        // GET: api/Comments/{id}
        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        // GET: api/Comments/movie/{movieId}
        [HttpGet("movie/{movieId}")]
        public ActionResult<IEnumerable<object>> GetCommentsByMovieId(int movieId)
        {
            // Truy vấn lấy bình luận cùng với FullName của người dùng
            var comments = _context.Comments
                .Include(c => c.UsernameNavigation) // Bao gồm thông tin người dùng
                .ThenInclude(u => u.DetailUser) // Bao gồm thông tin DetailUser
                .Where(c => c.CourseId == movieId)
                .Select(c => new
                {
                    c.CommentId,
                    c.CourseId,
                    c.Content,
                    FullName = c.UsernameNavigation.DetailUser.FullName, // Lấy FullName
                    c.CreatedAt,
                    c.UpdatedAt
                })
                .ToList();

            return comments;
        }

        // DELETE: api/Comments/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound(); // Trả về mã 404 nếu không tìm thấy bình luận
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return NoContent(); // Trả về mã 204 No Content
        }

    }
}
