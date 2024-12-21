using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO.Admin.RolesFilm
{
    public class RolesAdminDto
    {
        [JsonIgnore]
        public int RoleCourseId { get; set; }

        public string RoleName { get; set; } = null!;
    }
}
