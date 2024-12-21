using CNTT36_WebXemPhim.DTO.Customer.Course;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO
{
    public class UpdateUserDto
    {
        public string FullName { get; set; }

        [Required]
        public string DateOfBirth { get; set; } 
        public string Gender { get; set; }
        public IFormFile AvatarFile { get; set; }  // Thay đổi đây để nhận file ảnh
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
