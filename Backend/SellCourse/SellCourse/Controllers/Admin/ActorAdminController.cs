using CNTT36_WebXemPhim.DTO.Admin.Teacher;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorAdminController : ControllerBase
    {
        private readonly ITeacherRepository actorRepository;

        public ActorAdminController(ITeacherRepository actorRepository)
        {
            this.actorRepository = actorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            var actors = await actorRepository.GetAllTeachersAsync();
            return Ok(actors);
        }

        [HttpPost("CreateTeacher")]
        public async Task<IActionResult> CreateActor([FromBody] CreateTeacherDto dto)
        {
            var createActor = await actorRepository.CreateTeacherAsync(dto);

            return Created("", createActor);
        }

  

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteActor([FromRoute] int id)
        {
            var deleteActor = await actorRepository.DeleteTeacherAsync(id);

            if (deleteActor == null)
                return NotFound();

            return Ok(deleteActor);
        }
    }
}
