using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;

using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsRoleFilmController : ControllerBase
    {
        private readonly ICourseRepository _movieRepository;
        private readonly IRoleCourseRepository _roleFilmRepository;
        private readonly ITeachersRoleCourseRepository _actorsRoleFilmRepository;
        private readonly ITeacherRepository _teacherRepository;

        //public ActorsRoleFilmController(ITeacherRepository teacherRepository)
        //{
        //    this._teacherRepository = teacherRepository;
        //}

        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await _teacherRepository.GetAllTeachersAsync();
            return Ok(result);
        }

        public ActorsRoleFilmController(ITeacherRepository teacherRepository, ICourseRepository movieRepository,
            IRoleCourseRepository roleFilmRepository, ITeachersRoleCourseRepository actorsRoleFilmRepository)
        {
            _teacherRepository = teacherRepository;
            _movieRepository = movieRepository;
            _roleFilmRepository = roleFilmRepository;
            _actorsRoleFilmRepository = actorsRoleFilmRepository;
        }


        [HttpGet("movies")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieRepository.GetAllCourseAsync();
            return Ok(movies);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleFilmRepository.GetAllRoleFilmAsync();
            return Ok(roles);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateActorsRoleFilm([FromBody] CreateActorsRoleFilmDto dto)
        {
            var result = await _actorsRoleFilmRepository.CreateActorsRoleFilmAsync(dto);
            return Ok(result);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetActorRoleFilmDetails()
        {
            var details = await _actorsRoleFilmRepository.GetActorRoleFilmDetailsAsync();
            return Ok(details);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateActorRoleFilm(int id, [FromBody] UpdateActorRoleFilmDto dto)
        {
            var updatedActorRoleFilm = await _actorsRoleFilmRepository.UpdateActorRoleFilmAsync(id, dto);
            return Ok(updatedActorRoleFilm);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteActorRoleFilm(int id)
        {
            var deletedActorRoleFilm = await _actorsRoleFilmRepository.DeleteActorRoleFilmAsync(id);
            return Ok(deletedActorRoleFilm);
        }
    }
}
