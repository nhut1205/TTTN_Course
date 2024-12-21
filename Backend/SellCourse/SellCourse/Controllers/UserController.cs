using Microsoft.AspNetCore.Mvc;
using CNTT36_WebXemPhim.Models;
using Microsoft.EntityFrameworkCore;
using CNTT36_WebXemPhim.DTO;

namespace CNTT36_WebXemPhim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly SellCourseContext _context;

        public UserController(SellCourseContext context)
        {
            _context = context;
        }

        // Tìm kiếm chi tiết người dùng dựa trên username
        [HttpGet("detail/{username}")]
        public IActionResult GetUserDetail(string username)
        {
            // Tìm người dùng theo username và bao gồm thông tin chi tiết
            var user = _context.Users
                .Include(u => u.DetailUser) // Load thông tin chi tiết người dùng
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Lấy chi tiết người dùng từ thuộc tính DetailUser
            var userDetails = user.DetailUser;

            if (userDetails == null)
            {
                return NotFound(new { message = "User details not found" });
            }

            return Ok(userDetails);
        }


        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUserAsync(string username, [FromForm] UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null)
            {
                return BadRequest(new { Message = "updateUserDto is required." });
            }

            // Tìm User theo username
            var user = await _context.Users
                .Include(u => u.DetailUser) // Bao gồm DetailUser để cập nhật thông tin chi tiết
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Cập nhật thông tin DetailUser (nếu có)
            if (user.DetailUser != null)
            {
                if (!string.IsNullOrEmpty(updateUserDto.FullName))
                {
                    user.DetailUser.FullName = updateUserDto.FullName;
                }

                if (updateUserDto.AvatarFile != null && updateUserDto.AvatarFile.Length > 0)
                {
                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    // Kiểm tra và tạo thư mục uploads nếu chưa tồn tại
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    // Tạo tên file duy nhất bằng cách băm tên file gốc
                    var uniqueFileName = $"{Path.GetFileNameWithoutExtension(updateUserDto.AvatarFile.FileName)}_{Guid.NewGuid()}{Path.GetExtension(updateUserDto.AvatarFile.FileName)}";
                    var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

                    // Lưu file vào thư mục uploads
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateUserDto.AvatarFile.CopyToAsync(stream);
                    }

                    // Cập nhật URL đến ảnh
                    user.DetailUser.AvatarUrl = $"/uploads/{uniqueFileName}";
                }

                // Cập nhật các trường khác
                if (!string.IsNullOrEmpty(updateUserDto.DateOfBirth))
                {
                    user.DetailUser.DateOfBirth = DateOnly.Parse(updateUserDto.DateOfBirth);
                }

                if (!string.IsNullOrEmpty(updateUserDto.Gender))
                {
                    user.DetailUser.Gender = updateUserDto.Gender;
                }

                if (!string.IsNullOrEmpty(updateUserDto.PhoneNumber))
                {
                    user.DetailUser.PhoneNumber = updateUserDto.PhoneNumber;
                }

                if (!string.IsNullOrEmpty(updateUserDto.Address))
                {
                    user.DetailUser.Address = updateUserDto.Address;
                }
            }

            // Cập nhật thời gian sửa đổi
            user.UpdatedAt = DateTime.Now;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User information updated successfully" });
        }

    }
}
