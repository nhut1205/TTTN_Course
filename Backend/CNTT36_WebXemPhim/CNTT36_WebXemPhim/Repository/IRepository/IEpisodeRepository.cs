using CNTT36_WebXemPhim.DTO.Admin.Episode;
using CNTT36_WebXemPhim.DTO.Customer;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface IEpisodeRepository
    {
        Task<List<EpisodeAdminDto>> GetAllEpisodesAsync();
        Task<EpisodeAdminDto> CreateEpisodeAsync(CreateEpisodeDto dto);
        Task<EpisodeAdminDto?> UpdateEpisodeAsync(int episodeId, UpdateEpisodeDto dto);
        Task<EpisodeAdminDto?> DeleteEpisodeByIdAsync(int id);
    }
}
