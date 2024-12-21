using AutoMapper;
using CNTT36_WebXemPhim.DTO.Customer.Course;
using CNTT36_WebXemPhim.DTO.Customer.Genre;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace CNTT36_WebXemPhim.Repository
{
    public class CateRepository : ICateRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public CateRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<CateDto>> GetAllGenresAsync()
        {       
            var genreDomain = await dbContext.Categories.ToListAsync();
            return mapper.Map<List<CateDto>>(genreDomain);
        }

        public async Task<CateWithCoursesDto?> GetByIdAsync(int id)
        {
            var genre = await dbContext.Categories
                .Include(g => g.CourseCates)
                    .ThenInclude(mg => mg.Course)
                .FirstOrDefaultAsync(g => g.CateId == id);

            if (genre == null)
                return null;

            var cateWithCourseDto = mapper.Map<CateWithCoursesDto>(genre);
            return cateWithCourseDto;
        }
    }
}
