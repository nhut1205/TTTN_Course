using AutoMapper;
using CNTT36_WebXemPhim.DTO.Admin.Actor;
using CNTT36_WebXemPhim.DTO.Admin.Teacher;
using CNTT36_WebXemPhim.DTO.Customer;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public TeacherRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto dto)
        {
            var teacher = mapper.Map<Teacher>(dto);

            await dbContext.Teachers.AddAsync(teacher);
            await dbContext.SaveChangesAsync();

            var teacherDto = mapper.Map<TeacherDto>(teacher);
            return teacherDto;
        }

        public async Task<TeacherDto?> DeleteTeacherAsync(int id)
        {
            var teacher = await dbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with Id {id} was not found.");
            }

            dbContext.Teachers.Remove(teacher);
            await dbContext.SaveChangesAsync();
            var teacherDto = mapper.Map<TeacherDto>(teacher);
            return teacherDto;
        }


        //public async Task<TeacherDto?> UpdateTeacherAsync(int id, UpdateActorDto dto)
        //{
        //    var teacher = await dbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id)
        //        if (teacher == null)
        //    {
        //        throw new KeyNotFoundException($"Teacher with Id {id} was not found.");
        //    }

        //}

        public async Task<List<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await dbContext.Teachers.ToListAsync();
            return mapper.Map<List<TeacherDto>>(teachers);
        }

    }
}
