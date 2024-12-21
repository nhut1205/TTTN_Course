namespace CNTT36_WebXemPhim.DTO.Admin.Teacher
{
    public class CreateTeacherDto
    {
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
