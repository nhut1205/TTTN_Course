using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO.Admin.User
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string MembershipStatus { get; set; } = "Free";

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

        public string Created_At => CreatedAt.ToString("dd/MM/yyyy");
        public string Updated_At => UpdatedAt.ToString("dd/MM/yyyy");

        public List<UserRoleDto> UserRoles { get; set; }
    }
}
