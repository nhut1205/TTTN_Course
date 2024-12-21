using AutoMapper;
using CNTT36_WebXemPhim.DTO.Admin.Actor;
using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;
using CNTT36_WebXemPhim.DTO.Admin.RolesFilm;
using CNTT36_WebXemPhim.DTO.Customer;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Repository
{
    public class RoleCourseRepository : IRoleCourseRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public RoleCourseRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<RoleFilmDto>> GetAllRoleFilmAsync()
        {
            var roleFilmDomain = await dbContext.RoleCourses.ToListAsync();
            return mapper.Map<List<RoleFilmDto>>(roleFilmDomain);
        }

        public async Task<RoleFilmDto> CreateRolesFilmAsysnc(CreateRolesCourseDto dto)
        {
            var roleFilmAdmin = mapper.Map<RoleCourse>(dto);

            var value = await dbContext.RoleCourses.AddAsync(roleFilmAdmin);
            await dbContext.SaveChangesAsync();

            var roleFilmAdminDto = mapper.Map<RoleFilmDto>(roleFilmAdmin);
            return roleFilmAdminDto;
        }


        public async Task<RoleFilmDto> UpdateRolesFilmAsyns(int id, UpdateRolesCourseDto dto)
        {
            var rolesId = await dbContext.RoleCourses.FirstOrDefaultAsync(e => e.RoleCourseId == id);
            if (rolesId == null)
            {
                throw new KeyNotFoundException($"Role với Id {rolesId} không tồn tại.");
            }

            mapper.Map(dto, rolesId);
            await dbContext.SaveChangesAsync();
            return mapper.Map<RoleFilmDto>(rolesId);
        }

        public async Task<RoleFilmDto> DeleteRolesFilmAsyns(int id)
        {
            var roleFilm = await dbContext.RoleCourses.FirstOrDefaultAsync(x => x.RoleCourseId == id);
            if (roleFilm == null)
            {
                throw new KeyNotFoundException($"Role Film with Id {id} was not found.");
            }

            dbContext.RoleCourses.Remove(roleFilm);
            await dbContext.SaveChangesAsync();
            var roleFilmDto = mapper.Map<RoleFilmDto>(roleFilm);
            return roleFilmDto;
        }


    }
}
