using AutoMapper;
using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;
using CNTT36_WebXemPhim.DTO.Admin.Episode;
using CNTT36_WebXemPhim.DTO.Customer;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Repository
{
    public class EpisodeRepository: IEpisodeRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public EpisodeRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<EpisodeAdminDto>> GetAllEpisodesAsync()
        {
            var episodes = await dbContext.Lessons.Include(e => e.Course).ToListAsync();
            return mapper.Map<List<EpisodeAdminDto>>(episodes);
        }

        public async Task<EpisodeAdminDto> CreateEpisodeAsync(CreateEpisodeDto dto)
        {
            var episodeAdminFilm = mapper.Map <Lesson>(dto);

            await dbContext.Lessons.AddAsync(episodeAdminFilm);
            await dbContext.SaveChangesAsync();

            var episodeAdminFilmDto = mapper.Map<EpisodeAdminDto>(episodeAdminFilm);
            return episodeAdminFilmDto;
        }

        public async Task<EpisodeAdminDto?> UpdateEpisodeAsync(int episodeId, UpdateEpisodeDto dto)
        {
            var episode = await dbContext.Lessons.FirstOrDefaultAsync(e => e.LessonId == episodeId);
            if (episode == null)
            {
                throw new KeyNotFoundException($"Episode với Id {episodeId} không tồn tại.");
            }

            mapper.Map(dto, episode);
            await dbContext.SaveChangesAsync();
            return mapper.Map<EpisodeAdminDto>(episode);
        }

        public async Task<EpisodeAdminDto?> DeleteEpisodeByIdAsync(int id)
        {
            var episode = await dbContext.Lessons.FirstOrDefaultAsync(e => e.LessonId == id);

            if (episode == null)
            {
                throw new KeyNotFoundException($"Episode với Id {id} không tồn tại.");
            }

            dbContext.Lessons.Remove(episode);
            await dbContext.SaveChangesAsync();

            var episodeDto = mapper.Map<EpisodeAdminDto>(episode);
            return episodeDto;
        }
    }
}
