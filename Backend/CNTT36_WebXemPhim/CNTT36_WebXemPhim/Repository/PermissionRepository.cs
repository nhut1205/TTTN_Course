//using AutoMapper;
//using CNTT36_WebXemPhim.Repository.IRepository;
//using Microsoft.EntityFrameworkCore;
//using CNTT36_WebXemPhim.Models;

//namespace CNTT36_WebXemPhim.Repository
//{
//    public class PermissionRepository : IPermissionRepository
//    {
//        private readonly SellCourseContext dbContext;
//        private readonly IMapper mapper;

//        public PermissionRepository(SellCourseContext dbContext, IMapper mapper)
//        {
//            this.dbContext = dbContext;
//            this.mapper = mapper;
//        }

//        // Kiểm tra quyền
//        public async Task<bool> HasPermissionAsync(string username, string resource, string action)
//        {
//            // Lấy roleId của user
//            var userRole = await dbContext.UserRoles
//                .Include(ur => ur.Role)
//                .FirstOrDefaultAsync(ur => ur.Username == username);

//            if (userRole == null)
//                return false;

//            // Lấy quyền trong AdminPermission
//            var permission = await dbContext.AdminPermissions
//                .FirstOrDefaultAsync(ap =>
//                    ap.AdminRoleId == userRole.RoleId &&
//                    ap.Resource == resource);

//            if (permission == null)
//                return false;

//            // Kiểm tra hành động
//            return action switch
//            {
//                "CREATE" => permission.CanCreate ?? false,
//                "READ" => permission.CanRead ?? false,
//                "UPDATE" => permission.CanUpdate ?? false,
//                "DELETE" => permission.CanDelete ?? false,
//                _ => false
//            };
//        }
//    }
//}
