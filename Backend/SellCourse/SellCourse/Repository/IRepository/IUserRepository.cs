using CNTT36_WebXemPhim.DTO.Admin.User;
using CNTT36_WebXemPhim.DTO.Customer.Course;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsersAsync();
    }
}
