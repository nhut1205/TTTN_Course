using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriController : ControllerBase
    {
        private readonly ICateRepository cateRepository;
        private readonly SellCourseContext _context;

        public CategoriController(ICateRepository genreRepository, SellCourseContext context)
        {
            this.cateRepository = genreRepository;
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var home = await cateRepository.GetAllGenresAsync();
            return Ok(home);
        }

        [HttpGet("{id}")]
        public IActionResult GetCoursesByCategory(int id)
        {
            var courses = _context.CourseCates
                                  .Where(cc => cc.CateId == id)
                                  .Include(cc => cc.Course)
                                  .Include(cc => cc.Cate)
                                  .ToList();

            if (!courses.Any())
            {
                return NotFound();
            }
            return Ok(courses);
        }


    }
}