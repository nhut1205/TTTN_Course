using CNTT36_WebXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly SellCourseContext _context;

        public WatchlistController(SellCourseContext context)
        {
            _context = context;
        }

        // POST: api/Watchlist/Add
        [HttpPost("Add")]
        public IActionResult AddToWatchlist([FromBody] WatchlistRequest request)
        {
            
            var username = request.Username;
            var courseId = request.courseId;

            if (username == null || courseId == null)
            {
                return NotFound("User or Courser not found");
            }

            // Check if already in watchlist
            var existingWatchlistItem = _context.Watchlists.FirstOrDefault(w => w.Username == username && w.CourseId == courseId);
            if (existingWatchlistItem != null)
            {
                return BadRequest("Course already in watchlist");
            }

            // Create a new watchlist entry
            var watchlistItem = new Watchlist
            {
                Username = username,
                CourseId = courseId,
                AddedAt = DateTime.UtcNow
            };

            _context.Watchlists.Add(watchlistItem);
            _context.SaveChanges();

            return Ok("Course added to watchlist successfully");
        }

        // Add the following methods to WatchlistController

        // GET: api/Watchlist/CheckLikeStatus
        [HttpGet("CheckLikeStatus")]
        public IActionResult CheckLikeStatus(string username, int courseId)
        {
            var watchlistItem = _context.Watchlists.FirstOrDefault(w => w.Username == username && w.CourseId == courseId);
            if (watchlistItem != null)
            {
                return Ok(new { liked = true });
            }
            return Ok(new { liked = false });
        }


        // POST: api/Watchlist/ToggleLike
        [HttpPost("ToggleLike")]
        public IActionResult ToggleLike([FromBody] WatchlistRequest request)
        {
            var user = _context.Users.Find(request.Username);
            var course = _context.Courses.Find(request.courseId);

            if (user == null || course == null)
            {
                return NotFound("User or course not found");
            }

            var existingWatchlistItem = _context.Watchlists.FirstOrDefault(w => w.Username == request.Username && w.CourseId == request.courseId);

            if (existingWatchlistItem != null)
            {
                _context.Watchlists.Remove(existingWatchlistItem);
                _context.SaveChanges();
                return Ok(new { liked = false });
            }
            else
            {
                var watchlistItem = new Watchlist
                {
                    Username = request.Username,
                    CourseId = request.courseId,
                    AddedAt = DateTime.UtcNow
                };

                _context.Watchlists.Add(watchlistItem);
                _context.SaveChanges();
                return Ok(new { liked = true });
            }
        }

        // Define a model for the request data
        public class WatchlistRequest
        {
            public string Username { get; set; }
            public int courseId { get; set; }
        }


        // GET: api/Watchlist/UserWatchlist/{username}
        [HttpGet("UserWatchlist/{username}")]
        public IActionResult GetUserWatchlist(string username)
        {
            var userWatchlist = _context.Watchlists
                .Include(w => w.Course)
                .Where(w => w.Username == username)
                .Select(w => new
                {
                    courseId = w.CourseId,
                    courseTitle = w.Course.Title,
                    AddedAt = w.AddedAt
                })
                .ToList();

            if (!userWatchlist.Any())
            {
                return NotFound("No courses in the watchlist for this user.");
            }

            return Ok(userWatchlist);
        }

    }
}
