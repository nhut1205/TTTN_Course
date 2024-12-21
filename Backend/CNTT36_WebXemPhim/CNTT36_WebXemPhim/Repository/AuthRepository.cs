using AutoMapper;
using CNTT36_WebXemPhim.DTO;
using CNTT36_WebXemPhim.DTO.Admin.User;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using CNTT36_WebXemPhim.Models;

namespace CNTT36_WebXemPhim.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SellCourseContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthRepository(SellCourseContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Kiểm tra xem Username đã tồn tại chưa
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == registerDto.Username);

            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }

            // Kiểm tra mật khẩu và mật khẩu xác nhận
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                throw new Exception("Passwords do not match");
            }

            // Kiểm tra xem Email đã tồn tại chưa
            var existingUserByEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if (existingUserByEmail != null)
            {
                throw new Exception("Email already exists");
            }

            // Sử dụng AutoMapper để map từ DTO sang entity User
            var user = _mapper.Map<User>(registerDto);

            // Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
            user.Password = HashPassword(registerDto.Password);

            // Thêm người dùng mới vào bảng Users
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();


            var detailUser = new DetailUser
            {
                Username = user.Username,
                FullName = "no name",
                DateOfBirth = new DateOnly(1900, 1, 1),
                AvatarUrl = "https://png.pngtree.com/png-vector/20221114/ourmid/pngtree-user-icon-human-man-vector-png-image_34635470.png",
            };

            // Thêm đối tượng DetailUser vào bảng DetailUsers
            await _context.DetailUsers.AddAsync(detailUser);
            await _context.SaveChangesAsync();

            // Tìm vai trò "User" trong bảng Role
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "USER");

            // Nếu vai trò "USER" chưa tồn tại, tạo mới vai trò này
            if (userRole == null)
            {
                userRole = new Role
                {
                    RoleName = "USER"
                };

                // Thêm vai trò "USER" vào bảng Roles
                await _context.Roles.AddAsync(userRole);
                await _context.SaveChangesAsync(); // Lưu lại thay đổi vào cơ sở dữ liệu
            }

            // Gán vai trò "User" cho người dùng vừa tạo
            var userRoleLink = new UserRole
            {
                Username = user.Username, // ID của người dùng vừa tạo
                RoleId = userRole.RoleId // ID của vai trò "USER"
            };

            // Thêm vào bảng UserRoles để liên kết giữa người dùng và vai trò
            await _context.UserRoles.AddAsync(userRoleLink);
            await _context.SaveChangesAsync(); // Lưu lại thay đổi vào cơ sở dữ liệu

            // Trả về đối tượng UserDto (đã được map từ User)
            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // Kiểm tra username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username"); // Nếu username không hợp lệ
            }

            // Kiểm tra password
            if (!VerifyPassword(user.Password, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid password"); // Nếu password không hợp lệ
            }

            // Tạo token
            return GenerateToken(user); // Trả về token khi đăng nhập thành công
        }

        public async Task<string> LoginWithFacebookAsync(string token)
        {
            // Xác thực token với Facebook API để lấy thông tin người dùng
            var userInfo = await GetFacebookUserInfo(token);

            // Kiểm tra xem người dùng đã tồn tại trong cơ sở dữ liệu chưa
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userInfo.Email);
            if (user == null)
            {
                // Nếu người dùng chưa tồn tại, tạo tài khoản mới
                user = new User
                {
                    Username = userInfo.Name,
                    Email = userInfo.Email,
                    // Bạn có thể thêm các thông tin khác từ userInfo
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Tạo token
            return GenerateToken(user); // Trả về token khi đăng nhập thành công
        }


        private async Task<FacebookUserInfo> GetFacebookUserInfo(string token)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"https://graph.facebook.com/me?access_token={token}&fields=id,name,email");
                return JsonConvert.DeserializeObject<FacebookUserInfo>(response);
            }
        }

        public class FacebookUserInfo
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public async Task<string> LoginAdminAsync(LoginDto loginDto)
        {
            // Kiểm tra username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username"); // Nếu username không hợp lệ
            }

            // Kiểm tra password
            if (!VerifyPassword(user.Password, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid password"); // Nếu password không hợp lệ
            }

            // Kiểm tra nếu user là Admin
            var isAdmin = await _context.UserRoles.AnyAsync(ur => ur.Username == user.Username && ur.Role.RoleName == "ADMIN");

            if (!isAdmin)
            {
                throw new UnauthorizedAccessException("User is not an admin"); // Nếu không phải là admin
            }

            // Tạo token
            return GenerateToken(user); // Trả về token khi đăng nhập thành công
        }

        public async Task<bool> CheckAdminRoleAsync(string token)
        {
            var claimsPrincipal = ValidateToken(token);
            if (claimsPrincipal == null)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            // Kiểm tra claim "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            var isAdmin = claimsPrincipal.Claims.Any(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && c.Value == "ADMIN");

            return isAdmin;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("bbuosyJSSGPOosflUSJ75JJHst6yjjjST5rt65SY77uhSYSko098HHhgst"); // Khóa bí mật đã dùng để tạo JWT

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Tùy thuộc vào cách bạn tạo token
                    ValidateAudience = false, // Tùy thuộc vào cách bạn tạo token
                    ClockSkew = TimeSpan.Zero // Giảm thiểu sai lệch thời gian
                }, out SecurityToken validatedToken);

                // In ra các claims trong token
                foreach (var claim in principal.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }

                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string hashedPassword, string password)
        {
            var hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }

        private string GenerateToken(User user)
        {
            var userRoles = _context.UserRoles
                                    .Where(ur => ur.Username == user.Username)
                                    .Select(ur => ur.Role.RoleName)
                                    .ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Username", user.Username.ToString())
            };

            // Thêm các claims về vai trò của user
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDto> RegisterFromFacebookAsync(FacebookRegisterDto facebookRegisterDto)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == facebookRegisterDto.Email);

            if (existingUser != null)
            {
                // Nếu người dùng đã tồn tại, có thể trả về thông tin của người dùng
                return _mapper.Map<UserDto>(existingUser);
            }

            // Tạo người dùng mới từ thông tin Facebook
            var user = new User
            {
                Username = facebookRegisterDto.Username,
                Email = facebookRegisterDto.Email,
                Password = HashPassword(Guid.NewGuid().ToString()), // Mật khẩu có thể được tạo ngẫu nhiên
                                                                    // Thêm các thông tin khác nếu cần
            };

            // Thêm người dùng mới vào bảng Users
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Tạo đối tượng DetailUser nếu cần
            var detailUser = new DetailUser
            {
                Username = user.Username,
                FullName = facebookRegisterDto.FullName,
                DateOfBirth = facebookRegisterDto.DateOfBirth,
                AvatarUrl = facebookRegisterDto.AvatarUrl,
            };

            // Thêm đối tượng DetailUser vào bảng DetailUsers
            await _context.DetailUsers.AddAsync(detailUser);
            await _context.SaveChangesAsync();

            // Trả về đối tượng UserDto (đã được map từ User)
            return _mapper.Map<UserDto>(user);
        }

        #region reset-password
        public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            // Tìm người dùng theo Username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == changePasswordDto.Username);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Kiểm tra mật khẩu hiện tại
            if (!VerifyPassword(user.Password, changePasswordDto.CurrentPassword))
            {
                throw new Exception("Current password is incorrect");
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            {
                throw new Exception("New passwords do not match");
            }

            // Cập nhật mật khẩu mới
            user.Password = HashPassword(changePasswordDto.NewPassword);
            _context.Users.Update(user);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return true; // Trả về thành công
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdatePasswordAsync(User user, string hashedPassword)
        {
            user.Password = hashedPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
