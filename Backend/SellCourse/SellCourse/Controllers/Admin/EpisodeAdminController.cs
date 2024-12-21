using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;
using CNTT36_WebXemPhim.DTO.Admin.Episode;
using CNTT36_WebXemPhim.Repository;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeAdminController : ControllerBase
    {
        private readonly IEpisodeRepository episodeRepository;

        public EpisodeAdminController(IEpisodeRepository episodeRepository)
        {
            this.episodeRepository = episodeRepository;
        }

        [HttpGet("episodes")]
        public async Task<IActionResult> GetAllEpisodes()
        {
            var episodes = await episodeRepository.GetAllEpisodesAsync();
            return Ok(episodes);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEpisode([FromBody] CreateEpisodeDto dto)
        {
            var episode = await episodeRepository.CreateEpisodeAsync(dto);
            return Ok(episode);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEpíode(int id, [FromBody] UpdateEpisodeDto dto)
        {
            var updatedEpisode = await episodeRepository.UpdateEpisodeAsync(id, dto);
            return Ok(updatedEpisode);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            var deleteEpisode = await episodeRepository.DeleteEpisodeByIdAsync(id);
            return Ok(deleteEpisode);
        }
    }
}
