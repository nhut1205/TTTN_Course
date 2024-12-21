using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface ITeachersRoleCourseRepository
    {
        Task<ActorsRoleFilm2Dto> CreateActorsRoleFilmAsync(CreateActorsRoleFilmDto dto);
        Task<List<ActorRoleFilmDetailDto>> GetActorRoleFilmDetailsAsync();
        Task<ActorsRoleFilm2Dto?> UpdateActorRoleFilmAsync(int id, UpdateActorRoleFilmDto dto);
        Task<ActorsRoleFilm2Dto?> DeleteActorRoleFilmAsync(int id);
    }
}
