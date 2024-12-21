using AutoMapper;
using CNTT36_WebXemPhim.DTO;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CNTT36_WebXemPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
		private readonly IEmailRepository _emailRepository;

		public AuthController(IAuthRepository authRepository, IEmailRepository emailRepository)
        {
            _authRepository = authRepository;
            _emailRepository = emailRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await _authRepository.RegisterAsync(registerDto);
            return CreatedAtAction(nameof(Register), new { id = user.Username }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authRepository.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }

        [HttpPost("loginFacebook")]
        public async Task<IActionResult> LoginFacebook([FromBody] FacebookLoginDto facebookLoginDto)
        {
            // Giả sử bạn đã có phương thức xử lý đăng nhập Facebook
            //var token = await _authRepository.LoginWithFacebookAsync(facebookLoginDto.Token, facebookLoginDto.Username);
            var token = await _authRepository.LoginWithFacebookAsync(facebookLoginDto.Token);
            return Ok(new { Token = token });
        }


        [HttpPost("CheckAdmin")]
        public async Task<IActionResult> CheckAdmin([FromBody] TokenDto tokenDto)
        {
            var isAdmin = await _authRepository.CheckAdminRoleAsync(tokenDto.Token);
            if (!isAdmin)
            {
                return Unauthorized("User is not an admin");
            }
            return Ok("User is an admin");
        }


        [HttpPost("loginAdmin")]
        public async Task<IActionResult> LoginAdmin(LoginDto loginDto)
        {
            var token = await _authRepository.LoginAdminAsync(loginDto);
            return Ok(new { Token = token });
        }

        // Phương thức đăng ký từ Facebook
        [HttpPost("register-facebook")]
        public async Task<IActionResult> RegisterFromFacebook([FromBody] FacebookRegisterDto facebookRegisterDto)
        {
            if (facebookRegisterDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var userDto = await _authRepository.RegisterFromFacebookAsync(facebookRegisterDto);
                return CreatedAtAction(nameof(RegisterFromFacebook), new { id = userDto.Username }, userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var result = await _authRepository.ChangePasswordAsync(changePasswordDto);
                if (result)
                {
                    return Ok(new { message = "Password changed successfully" });
                }
                return BadRequest(new { message = "Failed to change password" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
		{
			var user = await _authRepository.GetByEmailAsync(dto.Email);
			if (user == null)
			{
				return Ok("Nếu email tồn tại, mật khẩu mới đã được gửi."); // Tránh tiết lộ thông tin
			}

			// Tạo mật khẩu mới
			var newPassword = GenerateRandomPassword(8);
			var hashedPassword = HashPassword(newPassword);

			// Cập nhật mật khẩu trong DB
			await _authRepository.UpdatePasswordAsync(user, hashedPassword);

			// Gửi email
			var emailBody = $"<p>Mật khẩu mới của bạn là: <strong>{newPassword}</strong></p>";
			await _emailRepository.SendEmailAsync(dto.Email, "Đặt lại mật khẩu", emailBody);

			return Ok("Nếu email tồn tại, mật khẩu mới đã được gửi.");
		}

		private string GenerateRandomPassword(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}

		private string HashPassword(string password)
		{
			using var sha256 = SHA256.Create();
			var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			return Convert.ToBase64String(bytes);
		}
	}
}
