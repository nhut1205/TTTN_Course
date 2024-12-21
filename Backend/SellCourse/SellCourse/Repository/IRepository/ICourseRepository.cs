using CNTT36_WebXemPhim.DTO.Admin.Movie;
using CNTT36_WebXemPhim.DTO.Customer;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface ICourseRepository
    {
        Task<List<CourseDto>> GetAllCourseAsync(string? movieName = null);
        Task<CourseDto?> GetByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateMovieDto dto);
        Task<CourseDto?> UpdateCourseAsync(int id, UpdateMovieDto dto);
        Task<CourseDto?> DeleteCourseAsync(int id);
        Task<List<CourseDto>> GetCourseByTeacherAsync(int actorId);
        Task<List<CourseDto>> GetAllCourseAsync();
    }
}
