using AutoMapper;
using CNTT36_WebXemPhim.DTO.Customer;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using CNTT36_WebXemPhim.DTO.Admin.Movie;
namespace CNTT36_WebXemPhim.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public CourseRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<CourseDto>> GetAllCourseAsync(string? movieName = null)
        {
            // Bắt đầu với truy vấn lấy tất cả phim
            var query = dbContext.Courses.AsQueryable();

            // Nếu movieName có giá trị, lọc theo tên phim
            if (!string.IsNullOrEmpty(movieName))
            {
                // Sử dụng ToLower() để thực hiện so sánh không phân biệt hoa thường
                query = query.Where(m => m.Title.ToLower().Contains(movieName.ToLower()));
            }

            var queryDomain = await query.ToListAsync();
            return mapper.Map<List<CourseDto>>(queryDomain);
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var movie = await dbContext.Courses
                .Include(m => m.Lessons) // Eager load Episodes
                .Include(m => m.TeacherRoleCourses) // Eager load ActorsRoleFilms
                    .ThenInclude(ar => ar.Teacher) // Eager load Actor from ActorsRoleFilm
                .Include(m => m.CourseCates)
                    .ThenInclude(gr => gr.Cate)
                .FirstOrDefaultAsync(m => m.CourseId == id); // Tìm bộ khóa học theo ID

            if (movie == null) return null; // Nếu không tìm thấy, trả về null

            // Sử dụng AutoMapper để ánh xạ Movie thành MovieDto
            var movieDto = mapper.Map<CourseDto>(movie);
            return movieDto;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateMovieDto dto)
        {
            var movie = mapper.Map<Course>(dto);

            await dbContext.Courses.AddAsync(movie);
            await dbContext.SaveChangesAsync();

            var movieDto = mapper.Map<CourseDto>(movie);
            return movieDto;
        }

        public async Task<CourseDto?> UpdateCourseAsync(int id, UpdateMovieDto dto)
        {
            var movie = await dbContext.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Course with Id {id} was not found.");
            }

            mapper.Map(dto, movie);

            await dbContext.SaveChangesAsync();
            var movieDto = mapper.Map<CourseDto>(movie);
            return movieDto;
        }

        public async Task<CourseDto?> DeleteCourseAsync(int id)
        {
            var movie = await dbContext.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Course with Id {id} was not found.");
            }

            dbContext.Courses.Remove(movie);
            await dbContext.SaveChangesAsync();
            var movieDto = mapper.Map<CourseDto>(movie);
            return movieDto;
        }

        public async Task<List<CourseDto>> GetCourseByTeacherAsync(int teacherId)
        {
            // Tìm tất cả phim có sự tham gia của diễn viên với actorId
            var movies = await dbContext.Courses
                .Include(m => m.TeacherRoleCourses)
                .Where(m => m.TeacherRoleCourses.Any(ar => ar.TeacherId == teacherId))
                .ToListAsync();

            // Sử dụng AutoMapper để ánh xạ sang MovieDto
            return mapper.Map<List<CourseDto>>(movies);
        }

        public async Task<List<CourseDto>> GetAllCourseAsync()
        {
            var movieDomain = await dbContext.Courses.ToListAsync();
            return mapper.Map<List<CourseDto>>(movieDomain);
        }
    }
}
