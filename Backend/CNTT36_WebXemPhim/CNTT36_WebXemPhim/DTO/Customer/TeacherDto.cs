using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO.Customer
{
    public class TeacherDto
    {
        public int teacherId { get; set; }

        public string Name { get; set; } = null!;

        public string? Bio { get; set; }

        [JsonIgnore]
        public DateOnly DateOfBirth { get; set; }
        public string Date_Of_Birth => DateOfBirth.ToString("dd/MM/yyyy");

        public string? ProfilePictureUrl { get; set; }
    }
}
