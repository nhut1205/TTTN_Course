namespace CNTT36_WebXemPhim.DTO
{
    public class FacebookRegisterDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; } 
        public string AvatarUrl { get; set; } // URL của avatar
    }
}
