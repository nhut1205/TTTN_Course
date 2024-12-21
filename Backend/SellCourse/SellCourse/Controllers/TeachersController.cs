using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ICourseRepository movieRepository;

        public TeachersController(ICourseRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpGet("{teacherId}/movies")]
        public async Task<IActionResult> GetCoursesByActor(int teacherId)
        {
            var courses = await movieRepository.GetCourseByTeacherAsync(teacherId);
            if (courses == null || courses.Count == 0)
            {
                return NotFound(new { Message = "No movies found for this actor." });
            }
            return Ok(courses);
        }
    }

}
