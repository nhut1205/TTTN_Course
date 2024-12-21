namespace CNTT36_WebXemPhim.DTO.Admin.User
{
    public class UserRoleDto
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }

        public RoleDto Role { get; set; } // Chỉ chứa thông tin cơ bản của Role
    }
}
