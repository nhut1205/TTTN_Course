using AutoMapper;
using CNTT36_WebXemPhim.DTO.Admin.User;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await dbContext.Users
                .Include(u => u.UserRoles) // Bao gồm UserRoles
                .ThenInclude(ur => ur.Role) // Bao gồm Role trong UserRoles
                .Where(u => !u.UserRoles.Any(ur => ur.Role.RoleName == "ADMIN")) // Lọc ra tài khoản có vai trò Admin
                .ToListAsync();

            // Ánh xạ từ domain model sang DTO
            return mapper.Map<List<UserDto>>(users);
        }
    }
}
