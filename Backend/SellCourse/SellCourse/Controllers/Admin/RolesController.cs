using CNTT36_WebXemPhim.DTO.Admin.Actor;
using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;
using CNTT36_WebXemPhim.DTO.Admin.RolesFilm;
using CNTT36_WebXemPhim.Repository;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleCourseRepository rolesFilmRepository;

        public RolesController(IRoleCourseRepository rolesRepository)
        {
            this.rolesFilmRepository = rolesRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            var rolesFilm = await rolesFilmRepository.GetAllRoleFilmAsync();
            return Ok(rolesFilm);
        }

        [HttpPost("createRolesFilm")]

        public async Task<IActionResult> CreateRoles([FromBody] CreateRolesCourseDto dto)
        {
            var result = await rolesFilmRepository.CreateRolesFilmAsysnc(dto);
            return Ok(result);
        }

        [HttpPut("updateRolesFilm/{id}")]
        public async Task<IActionResult> UpdateRolesFilm(int id, [FromBody] UpdateRolesCourseDto dto)
        {
            var updatedRoleFilm = await rolesFilmRepository.UpdateRolesFilmAsyns(id, dto);
            return Ok(updatedRoleFilm);
        }

        [HttpDelete("deleteRolesFilm/{id}")]
        public async Task<IActionResult> DeleteRolesFilm(int id)
        {
            var deleteRoles = await rolesFilmRepository.DeleteRolesFilmAsyns(id);
            return Ok(deleteRoles);
        }
    }
}