
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICourseRepository movieRepository;

        public HomeController(ICourseRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index(string? movieName)
        {
            var home = await movieRepository.GetAllCourseAsync(movieName);
            return Ok(home);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Detail_Film([FromRoute] int id)
        {
            var detail = await movieRepository.GetByIdAsync(id);

            if (detail == null)
                return NotFound();

            // Trả về MovieDto đã được ánh xạ
            return Ok(detail);
        }
    }
}
