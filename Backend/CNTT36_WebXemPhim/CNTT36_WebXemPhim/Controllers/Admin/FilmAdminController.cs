using AutoMapper;
using CNTT36_WebXemPhim.DTO.Admin.Movie;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmAdminController : ControllerBase
    {
        private readonly ICourseRepository movieRepository;

        public FilmAdminController(ICourseRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await movieRepository.GetAllCourseAsync();
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetFilmById([FromRoute] int id)
        {
            var detail = await movieRepository.GetByIdAsync(id);
            if (detail == null)
                return NotFound();
            return Ok(detail);
        }

        [HttpPost("CreateMovie")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto dto)
        {
            var createdMovie = await movieRepository.CreateCourseAsync(dto);

            return Created("", createdMovie);
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieDto dto)
        {
            var updateMovie = await movieRepository.UpdateCourseAsync(id, dto);

            //Convert Domain Model to DTO
            return Ok(updateMovie);
        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            var deleteMovie = await movieRepository.DeleteCourseAsync(id);

            if (deleteMovie == null)
                return NotFound();

            return Ok(deleteMovie);
        }
    }
}
