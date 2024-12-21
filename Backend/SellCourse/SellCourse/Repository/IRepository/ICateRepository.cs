using CNTT36_WebXemPhim.DTO.Customer.Genre;
using CNTT36_WebXemPhim.DTO.Customer.Course;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface ICateRepository
    {
        Task<List<CateDto>> GetAllGenresAsync();
        Task<CateWithCoursesDto?> GetByIdAsync(int id);
    }
}
