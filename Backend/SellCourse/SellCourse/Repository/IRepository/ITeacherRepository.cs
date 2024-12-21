using CNTT36_WebXemPhim.DTO.Admin.Teacher;
using CNTT36_WebXemPhim.DTO.Customer;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface ITeacherRepository
    {
        Task<List<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto dto);
        //Task<TeacherDto?> UpdateTeacherAsync(int id, UpdateActorDto dto);
        Task<TeacherDto?> DeleteTeacherAsync(int id);
    }
}
