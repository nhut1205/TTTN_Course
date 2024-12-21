using System.ComponentModel.DataAnnotations;

namespace CNTT36_WebXemPhim.DTO
{
    public class RegisterDto
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "* Tên đăng nhập không để trống")]
        [MaxLength(20, ErrorMessage = "* Tên đăng nhập tối đa 20 kí tự")]
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "* Mật khẩu không để trống")]
        [MaxLength(30, ErrorMessage = "* Mật khẩu tối đa 30 kí tự")]
        public string Password { get; set; } = null!;

        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "* Vui lòng xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "* Xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
