using AutoMapper;
using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;
using CNTT36_WebXemPhim.DTO.Customer;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Repository
{
    public class TeachersRoleCoursesRepository : ITeachersRoleCourseRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public TeachersRoleCoursesRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ActorsRoleFilm2Dto> CreateActorsRoleFilmAsync(CreateActorsRoleFilmDto dto)
        {
            var teacherRoleCourse = mapper.Map<TeacherRoleCourse>(dto);

            await dbContext.Set<TeacherRoleCourse>().AddAsync(teacherRoleCourse);
            await dbContext.SaveChangesAsync();

            var actorRoleFilmDto = mapper.Map<ActorsRoleFilm2Dto>(teacherRoleCourse);
            return actorRoleFilmDto;
        }

        public async Task<List<ActorRoleFilmDetailDto>> GetActorRoleFilmDetailsAsync()
        {
            var actorRoleFilms = await dbContext.Set<TeacherRoleCourse>()
                .Include(arf => arf.Teacher)
                .Include(arf => arf.Course)
                .Include(arf => arf.RoleCourse)
                .ToListAsync();

            return mapper.Map<List<ActorRoleFilmDetailDto>>(actorRoleFilms);
        }

        public async Task<ActorsRoleFilm2Dto?> UpdateActorRoleFilmAsync(int id, UpdateActorRoleFilmDto dto)
        {
            var actorRoleFilm = await dbContext.Set<TeacherRoleCourse>()
                .Include(arf => arf.Teacher)
                .Include(arf => arf.Course)
                .Include(arf => arf.RoleCourse)
                .FirstOrDefaultAsync(arf => arf.TeacherRoleCourseId == id);

            if (actorRoleFilm == null)
            {
                throw new KeyNotFoundException("Teacher role courses not found");
            }

            mapper.Map(dto, actorRoleFilm);
            await dbContext.SaveChangesAsync();
            return mapper.Map<ActorsRoleFilm2Dto>(actorRoleFilm);
        }

        public async Task<ActorsRoleFilm2Dto?> DeleteActorRoleFilmAsync(int id)
        {
            var actorRoleFilm = await dbContext.Set<TeacherRoleCourse>()
                .FirstOrDefaultAsync(arf => arf.TeacherRoleCourseId == id);

            if (actorRoleFilm == null)
            {
                throw new KeyNotFoundException($"ActorRoleFilm with Id {id} was not found.");
            }

            dbContext.TeacherRoleCourses.Remove(actorRoleFilm);
            await dbContext.SaveChangesAsync();

            var actorRoleFilmDto = mapper.Map<ActorsRoleFilm2Dto>(actorRoleFilm);
            return actorRoleFilmDto;
        }
    }
}
