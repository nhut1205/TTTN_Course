using CNTT36_WebXemPhim.DTO.Admin.Actor;
using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;
using CNTT36_WebXemPhim.DTO.Admin.RolesFilm;
using CNTT36_WebXemPhim.DTO.Customer;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface IRoleCourseRepository
    {
        Task<List<RoleFilmDto>> GetAllRoleFilmAsync();
        Task<RoleFilmDto> CreateRolesFilmAsysnc(CreateRolesCourseDto dto);
        Task<RoleFilmDto> UpdateRolesFilmAsyns(int id, UpdateRolesCourseDto dto);
        Task<RoleFilmDto> DeleteRolesFilmAsyns(int id);
    }
}
