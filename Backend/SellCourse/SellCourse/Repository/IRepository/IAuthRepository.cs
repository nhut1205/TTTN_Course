using CNTT36_WebXemPhim.DTO;
using CNTT36_WebXemPhim.DTO.Admin.User;
using CNTT36_WebXemPhim.Models;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<string> LoginAdminAsync(LoginDto loginDto);
        Task<UserDto> RegisterFromFacebookAsync(FacebookRegisterDto facebookRegisterDto);
        //Task<string> LoginWithFacebookAsync(string token, string username); 
        Task<string> LoginWithFacebookAsync(string token);
        Task<bool> CheckAdminRoleAsync(string token);
        Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        Task<User?> GetByEmailAsync(string email);
        Task UpdatePasswordAsync(User user, string hashedPassword);
	}
}
